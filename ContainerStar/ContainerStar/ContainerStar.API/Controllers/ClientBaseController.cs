using System.Web.Http;
using ContainerStar.API.Models;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Managers.Base;

namespace ContainerStar.API.Controllers
{
    public abstract class ClientBaseController<TModel, TEntity, TId, TManager> :
        ClientBaseWithoutDeleteController<TModel, TEntity, TId, TManager>
        where TManager : IManagerBase<TEntity, TId>
        where TModel : class, IHasId<TId>, ISystemModelFields, new()
        where TEntity : class, IHasId<TId>, ISystemFields, IRemovable
    {
        protected ClientBaseController(TManager manager)
            : base(manager)
        {
        }

        public virtual IHttpActionResult Delete(TId id)
        {
            var entity = Manager.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            Manager.RemoveEntity(id);

            Manager.SaveChanges();

            OnActionSuccess(entity, ActionTypes.Delete);

            return Ok(id);
        }
    }
}