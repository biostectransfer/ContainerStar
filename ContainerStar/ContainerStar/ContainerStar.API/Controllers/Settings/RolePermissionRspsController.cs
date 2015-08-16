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
    ///     Controller for <see cref="RolePermissionRsp"/> entity
    /// </summary>
    public partial class RolePermissionRspsController: ClientApiController<RolePermissionRspModel, RolePermissionRsp, int, IRolePermissionRspManager>
    {

        public RolePermissionRspsController(IRolePermissionRspManager manager): base(manager){}

        protected override void EntityToModel(RolePermissionRsp entity, RolePermissionRspModel model)
        {
            model.roleId = entity.RoleId;
            model.permissionId = entity.PermissionId;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(RolePermissionRspModel model, RolePermissionRsp entity, ActionTypes actionType)
        {
            entity.RoleId = model.roleId;
            entity.PermissionId = model.permissionId;
        }
    }
}
