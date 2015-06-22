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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.TransportProducts })]
    /// <summary>
    ///     Controller for <see cref="TransportProducts"/> entity
    /// </summary>
    public partial class TransportProductsController: ClientApiController<TransportProductsModel, TransportProducts, int, ITransportProductsManager>
    {

        public TransportProductsController(ITransportProductsManager manager): base(manager){}

        protected override void EntityToModel(TransportProducts entity, TransportProductsModel model)
        {
            model.name = entity.Name;
            model.description = entity.Description;
            model.price = entity.Price;
            model.proceedsAccount = entity.ProceedsAccount;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(TransportProductsModel model, TransportProducts entity, ActionTypes actionType)
        {
            entity.Name = model.name;
            entity.Description = model.description;
            entity.Price = model.price;
            entity.ProceedsAccount = model.proceedsAccount;
        }
    }
}
