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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.ContainerTypes })]
    /// <summary>
    ///     Controller for <see cref="ContainerTypes"/> entity
    /// </summary>
    public partial class ContainerTypesController: ClientApiController<ContainerTypesModel, ContainerTypes, int, IContainerTypesManager>
    {

        public ContainerTypesController(IContainerTypesManager manager): base(manager){}

        protected override void EntityToModel(ContainerTypes entity, ContainerTypesModel model)
        {
            model.name = entity.Name;
            model.comment = entity.Comment;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(ContainerTypesModel model, ContainerTypes entity, ActionTypes actionType)
        {
            entity.Name = model.name;
            entity.Comment = model.comment;
        }
    }
}
