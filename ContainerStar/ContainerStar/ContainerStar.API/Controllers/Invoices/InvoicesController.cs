using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers;
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
            model.Id = entity.Id;
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
            model.isPayed = entity.PayDate.HasValue;
            model.payInDays = entity.PayInDays;
            model.payPartOne = entity.PayPartOne;
            model.payPartTwo = entity.PayPartTwo;
            model.payPartTree = entity.PayPartTree;

            CalculatePrices(entity, model);
        }

        private void CalculatePrices(ContainerStar.Contracts.Entities.Invoices entity, InvoicesModel model)
        {
            double totalPriceWithoutDiscountWithoutTax = 0;
            double totalPriceWithoutTax = 0;
            double totalPrice = 0;
            double summaryPrice = 0;

            CalculationHelper.CalculateInvoicePrices(entity, out totalPriceWithoutDiscountWithoutTax, out totalPriceWithoutTax,
                out totalPrice, out summaryPrice);

            model.totalPriceWithoutDiscountWithoutTax = totalPriceWithoutDiscountWithoutTax;
            model.totalPriceWithoutTax = totalPriceWithoutTax;
            model.totalPrice = totalPrice;
            model.summaryPrice = summaryPrice;
        }

        protected override void ModelToEntity(InvoicesModel model, ContainerStar.Contracts.Entities.Invoices entity, ActionTypes actionType)
        {
            entity.CreateDate = model.createDate;
            entity.ChangeDate = model.createDate;
            entity.WithTaxes = model.withTaxes;
            entity.Discount = model.discount;
            entity.ManualPrice = model.manualPrice;
            entity.PayInDays = model.payInDays;
            entity.PayPartOne = model.payPartOne;
            entity.PayPartTwo = model.payPartTwo;
            entity.PayPartTree = model.payPartTree;
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
            else if (filter.Field == "isPayed")
            {
                int payStatus;
                if (!String.IsNullOrEmpty(filter.Value))
                {
                    Int32.TryParse(filter.Value, out payStatus);

                    return String.Format("PayDate{0}", payStatus == 1 ? " == null" : " != null");
                }
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
