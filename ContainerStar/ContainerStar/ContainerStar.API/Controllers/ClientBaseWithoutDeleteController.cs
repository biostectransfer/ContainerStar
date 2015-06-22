using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using ContainerStar.API.Models;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Managers.Base;
using ContainerStar.Contracts.Exceptions;

namespace ContainerStar.API.Controllers
{
    public abstract class ClientBaseWithoutDeleteController<TModel, TEntity, TId, TManager> :
        ReadOnlyClientBaseController<TModel, TEntity, TId, TManager>
        where TManager : IManagerBase<TEntity, TId>
        where TModel : class, IHasId<TId>, ISystemModelFields, new()
        where TEntity : class, IHasId<TId>, IRemovable, ISystemFields
    {
        protected ClientBaseWithoutDeleteController(TManager manager)
            : base(manager)
        {
        }

        public event EventHandler<ActionSuccessEventArgs<TEntity, TId>> ActionSuccess;

        protected void OnActionSuccess(TEntity entity, ActionTypes actionType)
        {
            if (ActionSuccess != null)
            {
                ActionSuccess(this, new ActionSuccessEventArgs<TEntity, TId>
                {
                    ActionType = actionType,
                    Entity = entity
                });
            }
        }

        protected abstract void ModelToEntity(TModel model, TEntity entity, ActionTypes actionType);

        protected virtual void Validate(TModel model, TEntity entity, ActionTypes actionType)
        {
        }

        public virtual IHttpActionResult Put([FromBody] TModel model)
        {
            var entity = Manager.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            if (HasConcurrency(entity, model))
            {
                //TODO: put proper processor for concurrency
                return Conflict();
            }

            Validate(model, entity, ActionTypes.Update);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ModelToEntity(model, entity, ActionTypes.Update);
            SetChangeDate(entity);

            // TODO we should save FROM_DATE as "FROM_DATE 00:00:00"
            // TO_DATE as "TO_DATE 23:59:59"
            var sysModel = ((object) model) as IModelIntervalFields;
            var sysEntity = entity as IIntervalFields;
            if (sysEntity != null && sysModel != null)
            {
                sysEntity.ToDate = sysModel.toDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            try
            {
                Manager.SaveChanges();
            }
            catch(DuplicateEntityException ex)
            {
                SetDuplicateErrorsToModelState(ModelState, ex);
                return BadRequest(ModelState);
            }

            OnActionSuccess(entity, ActionTypes.Update);

            EntityToModel(entity, model);

            return Ok(model);
        }

        

        public virtual IHttpActionResult Post([FromBody] TModel model)
        {
            var entity = Manager.DataContext.CreateObject<TEntity>();
            Validate(model, entity, ActionTypes.Add);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ModelToEntity(model, entity, ActionTypes.Add);
            SetChangeDate(entity);
            Manager.AddEntity(entity);
            
            try
            {
                Manager.SaveChanges();
            }
            catch(DuplicateEntityException ex)
            {
                SetDuplicateErrorsToModelState(ModelState, ex);
                return BadRequest(ModelState);
            }

            model = new TModel
            {
                Id = entity.Id
            };

            OnActionSuccess(entity, ActionTypes.Add);

            EntityToModel(entity, model);

            return Ok(model);
        }

        private void SetDuplicateErrorsToModelState(ModelStateDictionary modelState, DuplicateEntityException exception)
        {
            foreach (var businessKey in exception.BusinessKeys)
            {
                modelState.AddModelError("model." + businessKey, "Die Entität muss im System eindeutig sein.");
            }
        }

        private void SetChangeDate(ISystemFields entity)
        {
            entity.ChangeDate = DateTime.Now;
        }

        private bool HasConcurrency(ISystemFields entity, ISystemModelFields model)
        {
            if (entity == null || 
                model == null ||
                entity.ChangeDate == DateTime.MinValue) //exclude tables without ChangeDate field. DateTime.MinValue is a default for them
            {
                return false;
            }
            var diff = entity.ChangeDate.Subtract(model.changeDate);
            return (diff.Seconds < -1 || diff.Seconds > 1);

        }
    }
}