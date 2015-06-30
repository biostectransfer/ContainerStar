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
    /// <summary>
    ///     Controller for <see cref="InvoicePositions"/> entity
    /// </summary>
    public partial class InvoicePositionsController: ClientApiController<InvoicePositionsModel, InvoicePositions, int, IInvoicePositionsManager>
    {

        public InvoicePositionsController(IInvoicePositionsManager manager): base(manager){}

        protected override void EntityToModel(InvoicePositions entity, InvoicePositionsModel model)
        {
            model.price = entity.Price;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
        }
        protected override void ModelToEntity(InvoicePositionsModel model, InvoicePositions entity, ActionTypes actionType)
        {
            entity.Price = model.price;
        }
    }
}
