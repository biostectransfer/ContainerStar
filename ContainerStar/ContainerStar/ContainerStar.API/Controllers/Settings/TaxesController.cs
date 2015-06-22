using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;

namespace ContainerStar.API.Controllers.Settings
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Taxes })]
    /// <summary>
    ///     Controller for <see cref="Taxes"/> entity
    /// </summary>
    public partial class TaxesController: ClientApiController<TaxesModel, Taxes, int, ITaxesManager>
    {

        public TaxesController(ITaxesManager manager): base(manager){}

        protected override void EntityToModel(Taxes entity, TaxesModel model)
        {
            model.value = entity.Value;
            model.fromDate = entity.FromDate;
            model.toDate = entity.ToDate;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(TaxesModel model, Taxes entity, ActionTypes actionType)
        {
            entity.Value = model.value;
            entity.FromDate = model.fromDate;
            entity.ToDate = model.toDate;
        }
    }
}
