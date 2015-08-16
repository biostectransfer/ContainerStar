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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Role })]
    /// <summary>
    ///     Controller for <see cref="Role"/> entity
    /// </summary>
    public partial class RolesController: ClientApiController<RoleModel, Role, int, IRoleManager>
    {

        public RolesController(IRoleManager manager): base(manager){}

        protected override void EntityToModel(Role entity, RoleModel model)
        {
            model.name = entity.Name;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(RoleModel model, Role entity, ActionTypes actionType)
        {
            entity.Name = model.name;
        }
    }
}
