using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;

namespace ContainerStar.API.Controllers.Invoices
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Invoices })]
    /// <summary>
    ///     Controller for <see cref="Invoices"/> entity
    /// </summary>
    public partial class InvoicesController: ClientApiController<InvoicesModel, ContainerStar.Contracts.Entities.Invoices, int, IInvoicesManager>
    {

        public InvoicesController(IInvoicesManager manager): base(manager){}

        protected override void EntityToModel(ContainerStar.Contracts.Entities.Invoices entity, InvoicesModel model)
        {
            model.invoiceNumber = entity.InvoiceNumber;
            model.payDate = entity.PayDate;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
            model.customerName = entity.Orders.CustomerName;
            model.communicationPartnerName = entity.Orders.CommunicationPartnerTitle;
        }
        protected override void ModelToEntity(InvoicesModel model, ContainerStar.Contracts.Entities.Invoices entity, ActionTypes actionType)
        {
            entity.InvoiceNumber = model.invoiceNumber;
            entity.PayDate = model.payDate;
        }
    }
}
