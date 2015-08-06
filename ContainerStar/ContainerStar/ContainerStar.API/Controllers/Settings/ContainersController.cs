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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Containers })]
    /// <summary>
    ///     Controller for <see cref="Containers"/> entity
    /// </summary>
    public partial class ContainersController: ClientApiController<ContainersModel, Containers, int, IContainersManager>
    {

        public ContainersController(IContainersManager manager): base(manager){}

        protected override void EntityToModel(Containers entity, ContainersModel model)
        {
            model.number = entity.Number;
            model.containerTypeId = entity.ContainerTypeId;
            model.length = entity.Length;
            model.width = entity.Width;
            model.height = entity.Height;
            model.color = entity.Color;
            model.price = entity.Price;
            model.proceedsAccount = entity.ProceedsAccount;
            model.isVirtual = entity.IsVirtual;
            model.manufactureDate = entity.ManufactureDate;
            model.boughtFrom = entity.BoughtFrom;
            model.boughtPrice = entity.BoughtPrice;
            model.comment = entity.Comment;
            model.sellPrice = entity.SellPrice;
            model.isSold = entity.IsSold;
            model.minPrice = entity.MinPrice;
            model.newContainerPrice = entity.NewContainerPrice;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(ContainersModel model, Containers entity, ActionTypes actionType)
        {
            entity.Number = model.number;
            entity.ContainerTypeId = model.containerTypeId;
            entity.Length = model.length;
            entity.Width = model.width;
            entity.Height = model.height;
            entity.Color = model.color;
            entity.Price = model.price;
            entity.ProceedsAccount = model.proceedsAccount;
            entity.IsVirtual = model.isVirtual;
            entity.ManufactureDate = model.manufactureDate;
            entity.BoughtFrom = model.boughtFrom;
            entity.BoughtPrice = model.boughtPrice;
            entity.Comment = model.comment;
            entity.SellPrice = model.sellPrice;
            entity.IsSold = model.isSold;
            entity.MinPrice = model.minPrice;
            entity.NewContainerPrice = model.newContainerPrice;

            ExtraModelToEntity(entity, model, actionType);
        }
    }
}
