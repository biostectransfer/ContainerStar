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
    /// <summary>
    ///     Controller for <see cref="ContainerTypeEquipmentRsp"/> entity
    /// </summary>
    public partial class ContainerTypeEquipmentRspsController: ClientApiController<ContainerTypeEquipmentRspModel, ContainerTypeEquipmentRsp, int, IContainerTypeEquipmentRspManager>
    {

        public ContainerTypeEquipmentRspsController(IContainerTypeEquipmentRspManager manager): base(manager){}

        protected override void EntityToModel(ContainerTypeEquipmentRsp entity, ContainerTypeEquipmentRspModel model)
        {
            model.containerTypeId = entity.ContainerTypeId;
            model.equipmentId = entity.EquipmentId;
            model.amount = entity.Amount;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(ContainerTypeEquipmentRspModel model, ContainerTypeEquipmentRsp entity, ActionTypes actionType)
        {
            entity.ContainerTypeId = model.containerTypeId;
            entity.EquipmentId = model.equipmentId;
            entity.Amount = model.amount;
        }
    }
}
