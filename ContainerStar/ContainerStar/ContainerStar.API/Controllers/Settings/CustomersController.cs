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
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Customers })]
    /// <summary>
    ///     Controller for <see cref="Customers"/> entity
    /// </summary>
    public partial class CustomersController: ClientApiController<CustomersModel, Customers, int, ICustomersManager>
    {

        public CustomersController(ICustomersManager manager): base(manager){}

        protected override void EntityToModel(Customers entity, CustomersModel model)
        {
            model.number = entity.Number;
            model.name = entity.Name;
            model.street = entity.Street;
            model.zip = entity.Zip;
            model.city = entity.City;
            model.country = entity.Country;
            model.phone = entity.Phone;
            model.mobile = entity.Mobile;
            model.fax = entity.Fax;
            model.email = entity.Email;
            model.comment = entity.Comment;
            model.iban = entity.Iban;
            model.bic = entity.Bic;
            model.withTaxes = entity.WithTaxes;
            model.autoDebitEntry = entity.AutoDebitEntry;
            model.autoBill = entity.AutoBill;
            model.discount = entity.Discount;
            model.ustId = entity.UstId;
            model.bank = entity.Bank;
            model.accountNumber = entity.AccountNumber;
            model.blz = entity.Blz;
            model.isProspectiveCustomer = entity.IsProspectiveCustomer;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(CustomersModel model, Customers entity, ActionTypes actionType)
        {
            entity.Number = model.number;
            entity.Name = model.name;
            entity.Street = model.street;
            entity.Zip = model.zip;
            entity.City = model.city;
            entity.Country = model.country;
            entity.Phone = model.phone;
            entity.Mobile = model.mobile;
            entity.Fax = model.fax;
            entity.Email = model.email;
            entity.Comment = model.comment;
            entity.Iban = model.iban;
            entity.Bic = model.bic;
            entity.WithTaxes = model.withTaxes;
            entity.AutoDebitEntry = model.autoDebitEntry;
            entity.AutoBill = model.autoBill;
            entity.Discount = model.discount;
            entity.UstId = model.ustId;
            entity.Bank = model.bank;
            entity.AccountNumber = model.accountNumber;
            entity.Blz = model.blz;
            entity.IsProspectiveCustomer = model.isProspectiveCustomer;
        }
    }
}
