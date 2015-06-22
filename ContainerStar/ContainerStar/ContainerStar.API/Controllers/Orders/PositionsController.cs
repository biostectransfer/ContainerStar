using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.Linq;
using System.Linq.Dynamic;

namespace ContainerStar.API.Controllers
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    /// <summary>
    ///     Controller for <see cref="Positions"/> entity
    /// </summary>
    public partial class PositionsController: ClientApiController<PositionsModel, Positions, int, IPositionsManager>
    {
        public PositionsController(IPositionsManager manager) : base(manager) { }

        protected override void EntityToModel(Positions entity, PositionsModel model)
        {
            model.orderId = entity.OrderId;
            model.isSellOrder = entity.IsSellOrder;
            model.containerId = entity.ContainerId;
            model.additionalCostId = entity.AdditionalCostId;
            model.price = entity.Price;
            model.fromDate = entity.FromDate;
            model.toDate = entity.ToDate;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;

        }
        protected override void ModelToEntity(PositionsModel model, Positions entity, ActionTypes actionType)
        {
            entity.OrderId = model.orderId;
            entity.IsSellOrder = model.isSellOrder;
            entity.ContainerId = model.containerId;
            entity.AdditionalCostId = model.additionalCostId;
            entity.Price = model.price;
            entity.FromDate = model.fromDate;
            entity.ToDate = model.toDate;
        }
    }
}
