using System;
using ContainerStar.API.Models;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using System.Collections.Generic;
using System.Linq;
using CoreBase;

namespace ContainerStar.API.Controllers
{
    public partial class OrderContainerEquipmentsController : ClientApiController<OrderContainerEquipmentModel, OrderContainerEquipmentRsp, int, IOrderContainerEquipmentRspManager>
    {

        public OrderContainerEquipmentsController(IOrderContainerEquipmentRspManager manager) : base(manager) { }

        protected override void EntityToModel(OrderContainerEquipmentRsp entity, OrderContainerEquipmentModel model)
        {
            model.orderId = entity.OrderId;
            model.containerId = entity.ContainerId;
            model.equipmentId = entity.EquipmentId;
            model.amount = entity.Amount;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }

        protected override void ModelToEntity(OrderContainerEquipmentModel model, OrderContainerEquipmentRsp entity, ActionTypes actionType)
        {
            entity.OrderId = model.orderId;
            entity.ContainerId = model.containerId;
            entity.EquipmentId = model.equipmentId;
            entity.Amount = model.amount;
        }
    }
}
