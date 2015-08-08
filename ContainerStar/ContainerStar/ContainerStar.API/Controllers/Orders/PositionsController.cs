using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
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
        public IOrderContainerEquipmentRspManager OrderContainerEquipmentRspManager { get; set; }
        public IContainersManager ContainerManager { get; set; }

        public PositionsController(IPositionsManager manager, IOrderContainerEquipmentRspManager orderContainerEquipmentRspManager, 
            IContainersManager containersManager) : 
            base(manager)
        {
            this.OrderContainerEquipmentRspManager = orderContainerEquipmentRspManager;
            this.ContainerManager = containersManager;
        }

        protected override void EntityToModel(Positions entity, PositionsModel model)
        {
            model.orderId = entity.OrderId;
            model.isSellOrder = entity.IsSellOrder;
            model.containerId = entity.ContainerId;
            model.isMain = entity.IsMain;
            model.paymentType = entity.PaymentType;

            if (entity.ContainerId.HasValue)
            {
                model.description = String.Format("{0} {1}", entity.Containers.Number, entity.Containers.ContainerTypes.Name);

                if(entity.FromDate != DateTime.MinValue)
                    model.fromDate = entity.FromDate;

                if (entity.ToDate != DateTime.MinValue)
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

            model.isOffer = entity.Orders.IsOffer;
        }

        protected override void ModelToEntity(PositionsModel model, Positions entity, ActionTypes actionType)
        {
            entity.OrderId = model.orderId;
            entity.IsSellOrder = model.isSellOrder;
            entity.ContainerId = model.containerId;
            entity.AdditionalCostId = model.additionalCostId;
            entity.Price = model.price;
            entity.IsMain = model.isMain;
            entity.PaymentType = model.paymentType;

            if (model.containerId.HasValue)
                entity.Amount = 1;
            else
                entity.Amount = model.amount;

            if(actionType == ActionTypes.Add && model.containerId.HasValue)
            {
                entity.FromDate = model.fromDate.HasValue ? model.fromDate.Value.Date : DateTime.Now.Date;
                entity.ToDate = model.toDate.HasValue ? model.toDate.Value.Date : DateTime.Now.Date;

                var container = ContainerManager.GetById(model.containerId.Value);
                foreach (var equipment in container.ContainerEquipmentRsps)
                {
                    OrderContainerEquipmentRspManager.AddEntity(new OrderContainerEquipmentRsp()
                    {
                        Amount = equipment.Amount,
                        ContainerId = model.containerId.Value,
                        OrderId = model.orderId,
                        EquipmentId = equipment.EquipmentId
                    });
                }
            }
        }
    }
}
