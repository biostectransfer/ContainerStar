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

namespace ContainerStar.API.Controllers.Settings
{
    /// <summary>
    ///     Controller for <see cref="ContainerEquipmentRsp"/> entity
    /// </summary>
    public partial class ContainerEquipmentRspsController: ClientApiController<ContainerEquipmentRspModel, ContainerEquipmentRsp, int, IContainerEquipmentRspManager>
    {

        public ContainerEquipmentRspsController(IContainerEquipmentRspManager manager): base(manager){}

        protected override void EntityToModel(ContainerEquipmentRsp entity, ContainerEquipmentRspModel model)
        {
            model.containerId = entity.ContainerId;
            model.equipmentId = entity.EquipmentId;
            model.amount = entity.Amount;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(ContainerEquipmentRspModel model, ContainerEquipmentRsp entity, ActionTypes actionType)
        {
            entity.ContainerId = model.containerId;
            entity.EquipmentId = model.equipmentId;
            entity.Amount = model.amount;
        }
    }
}
