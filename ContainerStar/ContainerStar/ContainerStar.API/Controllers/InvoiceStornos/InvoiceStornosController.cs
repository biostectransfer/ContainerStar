using ContainerStar.API.Models;
using ContainerStar.API.Models.InvoiceStornos;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers;
using System;

namespace ContainerStar.API.Controllers.InvoiceStorno
{
    /// <summary>
    ///     Controller for Invoice Storno
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.InvoiceStornos })]
    public partial class InvoiceStornosController : ClientApiController<InvoiceStornosModel, InvoiceStornos, int, IInvoiceStornosManager>
    {

        public InvoiceStornosController(IInvoiceStornosManager manager) : base(manager) { }

        protected override void EntityToModel(InvoiceStornos entity, InvoiceStornosModel model)
        {
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
            model.price = entity.Price;
            model.proceedsAccount = entity.ProceedsAccount;
            model.invoiceId = entity.InvoiceId;
            model.freeText = entity.FreeText;
        }

        protected override void ModelToEntity(InvoiceStornosModel model, InvoiceStornos entity, ActionTypes actionType)
        {
            entity.Price = model.price;
            entity.ProceedsAccount = model.proceedsAccount;
            entity.InvoiceId = model.invoiceId;
            entity.FreeText = model.freeText;
        }
    }
}
