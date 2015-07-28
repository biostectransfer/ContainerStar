using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers;
using System;

namespace ContainerStar.API.Controllers.Invoices
{
    /// <summary>
    ///     Controller for <see cref="InvoicePositions"/> entity
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Invoices })]
    public partial class InvoicePositionsController: ClientApiController<InvoicePositionsModel, InvoicePositions, int, IInvoicePositionsManager>
    {

        public InvoicePositionsController(IInvoicePositionsManager manager): base(manager){}

        protected override void EntityToModel(InvoicePositions entity, InvoicePositionsModel model)
        {
            model.price = entity.Price;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
            model.amount = entity.Amount;
            model.paymentType = entity.PaymentType;

            if (entity.Positions.ContainerId.HasValue)
            {
                model.description = String.Format("{0} {1}", entity.Positions.Containers.Number, entity.Positions.Containers.ContainerTypes.Name);
                model.isCointainerPosition = true;
                model.price = entity.Price;

                model.totalPrice = CalculationHelper.CalculatePositionPrice(entity.Positions.IsSellOrder, entity.Price, entity.Amount, 
                    entity.FromDate, entity.ToDate, entity.Payment);

                if (!entity.Positions.IsSellOrder)
                {                    
                    model.fromDate = entity.FromDate;
                    model.toDate = entity.ToDate;
                }
            }

            if (entity.Positions.AdditionalCostId.HasValue)
            {
                model.totalPrice = model.price * model.amount;
                model.description = entity.Positions.AdditionalCosts.Name;
                model.isCointainerPosition = false;
            }
        }
        protected override void ModelToEntity(InvoicePositionsModel model, InvoicePositions entity, ActionTypes actionType)
        {
            entity.Price = model.price;

            if(model.fromDate.HasValue)
                entity.FromDate = model.fromDate.Value;

            if (model.toDate.HasValue)
                entity.ToDate = model.toDate.Value;

            entity.PaymentType = model.paymentType;
        }
    }
}
