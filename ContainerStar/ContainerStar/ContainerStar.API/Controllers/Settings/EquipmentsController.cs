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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Equipments })]
    /// <summary>
    ///     Controller for <see cref="Equipments"/> entity
    /// </summary>
    public partial class EquipmentsController: ClientApiController<EquipmentsModel, Equipments, int, IEquipmentsManager>
    {

        public EquipmentsController(IEquipmentsManager manager): base(manager){}

        protected override void EntityToModel(Equipments entity, EquipmentsModel model)
        {
            model.description = entity.Description;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(EquipmentsModel model, Equipments entity, ActionTypes actionType)
        {
            entity.Description = model.description;
        }
    }
}
