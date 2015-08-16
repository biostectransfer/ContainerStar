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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Permission })]
    /// <summary>
    ///     Controller for <see cref="Permission"/> entity
    /// </summary>
    public partial class PermissionsController: ClientApiController<PermissionModel, Permission, int, IPermissionManager>
    {

        public PermissionsController(IPermissionManager manager): base(manager){}

        protected override void EntityToModel(Permission entity, PermissionModel model)
        {
            model.name = entity.Name;
            model.description = entity.Description;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(PermissionModel model, Permission entity, ActionTypes actionType)
        {
            entity.Name = model.name;
            entity.Description = model.description;
        }
    }
}
