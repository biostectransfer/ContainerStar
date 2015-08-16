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
    ///     Controller for <see cref="CommunicationPartners"/> entity
    /// </summary>
    public partial class CommunicationPartnersController: ClientApiController<CommunicationPartnersModel, CommunicationPartners, int, ICommunicationPartnersManager>
    {

        public CommunicationPartnersController(ICommunicationPartnersManager manager): base(manager){}

        protected override void EntityToModel(CommunicationPartners entity, CommunicationPartnersModel model)
        {
            model.name = entity.Name;
            model.firstName = entity.FirstName;
            model.customerId = entity.CustomerId;
            model.phone = entity.Phone;
            model.mobile = entity.Mobile;
            model.fax = entity.Fax;
            model.email = entity.Email;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(CommunicationPartnersModel model, CommunicationPartners entity, ActionTypes actionType)
        {
            entity.Name = model.name;
            entity.FirstName = model.firstName;
            entity.CustomerId = model.customerId;
            entity.Phone = model.phone;
            entity.Mobile = model.mobile;
            entity.Fax = model.fax;
            entity.Email = model.email;
        }
    }
}
