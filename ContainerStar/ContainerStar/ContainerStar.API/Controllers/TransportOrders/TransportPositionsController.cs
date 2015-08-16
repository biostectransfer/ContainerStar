using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using CoreBase.Controllers;
using CoreBase.Entities;
using System;
using System.Linq;
using System.Linq.Dynamic;

namespace ContainerStar.API.Controllers
{
    /// <summary>
    ///     Controller for <see cref="TransportPositions"/> entity
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.TransportOrders })]
    public partial class TransportPositionsController: ClientApiController<TransportPositionsModel, TransportPositions, int, ITransportPositionsManager>
    {
        public TransportPositionsController(ITransportPositionsManager manager) : base(manager) { }

        protected override void EntityToModel(TransportPositions entity, TransportPositionsModel model)
        {
            model.transportOrderId = entity.TransportOrderId;
            model.transportProductId = entity.TransportProductId;
            model.description = entity.TransportProducts.Name;

            model.amount = entity.Amount;
            model.price = entity.Price;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;

        }
        protected override void ModelToEntity(TransportPositionsModel model, TransportPositions entity, ActionTypes actionType)
        {
            entity.TransportOrderId = model.transportOrderId;
            entity.TransportProductId = model.transportProductId;
            entity.Price = model.price;
            entity.Amount = model.amount;
        }
    }
}
