using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

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
            model.customerAddress = String.Format("{0}, {1} {2}", entity.Orders.Customers.Street, entity.Orders.Customers.Zip, entity.Orders.Customers.City);
            model.rentOrderNumber = entity.Orders.RentOrderNumber;
            model.communicationPartnerName = entity.Orders.CommunicationPartnerTitle;
            model.withTaxes = entity.WithTaxes;
            model.discount = entity.Discount;
            model.taxValue = entity.TaxValue;
            model.manualPrice = entity.ManualPrice;

            CalculatePrices(entity, model);
        }

        private void CalculatePrices(ContainerStar.Contracts.Entities.Invoices entity, InvoicesModel model)
        {
            model.totalPriceWithoutDiscountWithoutTax = 0;
            model.totalPriceWithoutTax = 0;
            model.totalPrice = 0;

            var allPositions = entity.InvoicePositions.Where(o => !o.DeleteDate.HasValue).ToList();
            //container prices
            foreach (var position in allPositions.Where(o => o.Positions.ContainerId.HasValue))
            {
                if (position.Positions.IsSellOrder)
                {
                    model.totalPriceWithoutDiscountWithoutTax += position.Price * (double)position.Positions.Amount;
                }
                else
                {
                    var duration = (position.ToDate - position.FromDate).Days + 1;

                    if(duration < 1)
                    {
                        duration = 1;
                    }

                    var dayPrice = position.Price / (double)30;

                    model.totalPriceWithoutDiscountWithoutTax += (double)position.Positions.Amount * (double)duration * dayPrice;
                }
            }

            //discount only for containers
            var discount = (model.totalPriceWithoutDiscountWithoutTax / (double)100) * entity.Discount;

            //additional cost prices
            foreach (var position in allPositions.Where(o => o.Positions.AdditionalCostId.HasValue))
            {
                model.totalPriceWithoutDiscountWithoutTax += position.Price * (double)position.Positions.Amount;
            }

            //discount
            model.totalPriceWithoutTax = model.totalPriceWithoutDiscountWithoutTax - discount;

            var taxValue = (model.totalPriceWithoutTax / (double)100) * entity.TaxValue;
            if (entity.WithTaxes)
            {
                //with taxes
                model.totalPrice = model.totalPriceWithoutTax + taxValue;
            }
            else
            {
                //without taxes
                model.totalPrice = model.totalPriceWithoutTax;
            }

            //override total price with manual price
            if (model.manualPrice.HasValue)
            {
                model.summaryPrice = model.manualPrice.Value;
            }
            else
            {
                model.summaryPrice = model.totalPrice;
            }
        }

        protected override void ModelToEntity(InvoicesModel model, ContainerStar.Contracts.Entities.Invoices entity, ActionTypes actionType)
        {
            entity.CreateDate = model.createDate;
            entity.ChangeDate = model.createDate;
            entity.WithTaxes = model.withTaxes;
            entity.Discount = model.discount;
            entity.ManualPrice = model.manualPrice;
        }

        protected override string BuildWhereClause<T>(Filter filter)
        {
            if (filter.Field == "name")
            {
                var clauses = new List<string>();
                
                return String.Format(
                    "Orders.RentOrderNumber.Contains(\"{0}\") or " +
                    "Orders.Customers.Name.Contains(\"{0}\") or " +
                    "InvoiceNumber.Contains(\"{0}\") or " +
                    "Orders.CommunicationPartners.Name.Contains(\"{0}\") or " +
                    "Orders.CommunicationPartners.FirstName.Contains(\"{0}\")", filter.Value);
            }

            return base.BuildWhereClause<T>(filter);
        }

        protected override IQueryable<ContainerStar.Contracts.Entities.Invoices> Sort(IQueryable<ContainerStar.Contracts.Entities.Invoices> entities, Sorting sorting)
        {
            if (sorting.Field == "customerName")
            {
                if (sorting.Direction == "desc")
                    return entities.OrderByDescending(o => o.Orders.Customers.Name);
                else
                    return entities.OrderBy(o => o.Orders.Customers.Name);
            }
            else if (sorting.Field == "communicationPartnerName")
            {
                if (sorting.Direction == "desc")
                    return entities.OrderByDescending(o => o.Orders.CommunicationPartners.Name).
                        ThenByDescending(o => o.Orders.CommunicationPartners.FirstName);
                else
                    return entities.OrderBy(o => o.Orders.CommunicationPartners.Name).
                        ThenBy(o => o.Orders.CommunicationPartners.FirstName);
            }
            else if (sorting.Field == "rentOrderNumber")
            {
                if (sorting.Direction == "desc")
                    return entities.OrderByDescending(o => o.Orders.RentOrderNumber);
                else
                    return entities.OrderBy(o => o.Orders.RentOrderNumber);
            }

            return base.Sort(entities, sorting);
        }
    }
}
