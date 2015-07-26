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
    /// <summary>
    ///     Controller for <see cref="Positions"/> entity
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public partial class PositionsController: ClientApiController<PositionsModel, Positions, int, IPositionsManager>
    {
        public PositionsController(IPositionsManager manager) : base(manager) { }

        protected override void EntityToModel(Positions entity, PositionsModel model)
        {
            model.orderId = entity.OrderId;
            model.isSellOrder = entity.IsSellOrder;
            model.containerId = entity.ContainerId;
            model.isMain = entity.IsMain;

            if(entity.ContainerId.HasValue)
            {
                model.description = String.Format("{0} {1}", entity.Containers.Number, entity.Containers.ContainerTypes.Name);
                model.fromDate = entity.FromDate;
                model.toDate = entity.ToDate;
            }

            model.additionalCostId = entity.AdditionalCostId;

            if (entity.AdditionalCostId.HasValue)
            {
                model.description = entity.AdditionalCosts.Name;
            }

            model.amount = entity.Amount;
            model.price = entity.Price;
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
            entity.FromDate = model.fromDate.HasValue ? model.fromDate.Value.Date : DateTime.Now.Date;
            entity.ToDate = model.toDate.HasValue ? model.toDate.Value.Date : DateTime.Now.Date;
            entity.IsMain = model.isMain;

            if (model.containerId.HasValue)
                entity.Amount = 1;
            else
                entity.Amount = model.amount;
        }
    }
}
