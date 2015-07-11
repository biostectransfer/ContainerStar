using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Exceptions;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using ContainerStar.Lib.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ContainerStar.API.Controllers.Invoices
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Invoices })]
    public partial class AddInvoicesController: ApiController
    {
        protected readonly IInvoicesManager invoicesManager;
        protected readonly IUniqueNumberProvider numberProvider;
        protected readonly IOrdersManager ordersManager;
        protected readonly ITaxesManager taxesManager;
        protected readonly IInvoicePositionsManager invoicePositionsManager;

        public AddInvoicesController(IInvoicesManager invoicesManager, IOrdersManager ordersManager,
            ITaxesManager taxesManager, IInvoicePositionsManager invoicePositionsManager, IUniqueNumberProvider numberProvider)
        {
            this.invoicesManager = invoicesManager;
            this.numberProvider = numberProvider;
            this.ordersManager = ordersManager;
            this.taxesManager = taxesManager;
            this.invoicePositionsManager = invoicePositionsManager;
        }

        public IHttpActionResult Post(AddInvoiceModel model)
        {
            var order = ordersManager.GetById(model.orderId);
            double taxValue = CalculationHelper.CalculateTaxes(taxesManager);

            var invoice = new ContainerStar.Contracts.Entities.Invoices()
            {
                Orders = order,
                TaxValue = taxValue,
                WithTaxes = order.Customers.WithTaxes,
                Discount = order.Discount ?? 0,
                CreateDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                IsSellInvoice = model.isSell,
                InvoicePositions = new List<InvoicePositions>()
            };


            if(AddInvoicePositions(model.isMonthlyInvoice, model.isSell, order, invoice))
            {
                invoice.InvoiceNumber = numberProvider.GetNextInvoiceNumber();

                invoicesManager.AddEntity(invoice);
                invoicesManager.SaveChanges();
            }

            model.Id = invoice.Id;
            return Ok(model);
        }

        protected bool AddInvoicePositions(bool isMonthlyInvoice, bool isSell, Orders order, Contracts.Entities.Invoices invoice)
        {
            var orderPositions = order.Positions.Where(o => !o.DeleteDate.HasValue);

            var allInvoicePositions = invoicePositionsManager.GetEntities(o => o.Positions.OrderId == order.Id && !o.DeleteDate.HasValue).ToList();

            bool hasOpenPositions = false;

            foreach (var orderPosition in orderPositions.Where(o => o.IsSellOrder == isSell))
            {
                var invoicePositions = allInvoicePositions.Where(o => o.PositionId == orderPosition.Id);
                var amount = 0;
                var fromDate = orderPosition.FromDate;
                var toDate = orderPosition.ToDate;

                if(orderPosition.AdditionalCosts != null)
                {
                    var oldAmount = invoicePositions.Sum(o => o.Amount);
                    if (orderPosition.Amount > oldAmount)
                    {
                        amount = orderPosition.Amount - oldAmount;
                    }
                }
                else if (orderPosition.Containers != null)
                {
                    amount = 1;

                    GetPeriod(isMonthlyInvoice, order, orderPosition, invoicePositions, ref fromDate, ref toDate, ref amount);
                }

                if (amount != 0)
                {
                    if (isSell && orderPosition.Containers != null && 
                        !orderPosition.Containers.IsVirtual)
                    {
                        orderPosition.Containers.IsSold = true;
                    }

                    var newPosition = new InvoicePositions()
                    {
                        Positions = orderPosition,
                        Invoices = invoice,
                        Price = orderPosition.Price,
                        Amount = amount,
                        CreateDate = DateTime.Now,
                        ChangeDate = DateTime.Now,
                        FromDate = fromDate,
                        ToDate = toDate,
                    };
                    invoice.InvoicePositions.Add(newPosition);
                    hasOpenPositions = true;
                }
            }

            return hasOpenPositions;
        }

        protected void GetPeriod(bool isMonthlyInvoice, Orders order, Positions orderPosition, IEnumerable<InvoicePositions> invoicePositions, 
            ref DateTime fromDate, ref DateTime toDate, ref int amount)
        {
            if (invoicePositions != null && invoicePositions.Count() != 0)
            {
                var maxDate = invoicePositions.Max(o => o.ToDate);               
                //if not auto prolongation dont add 
                if (!order.AutoProlongation &&
                     orderPosition.ToDate <= maxDate)
                {
                    amount = 0;
                    return;
                }

                //only if in current month doesnt exist invoices
                if(maxDate.Month == DateTime.Now.Month && maxDate.Year == DateTime.Now.Year)
                {
                    amount = 0;
                    return;
                }


                if (maxDate.Day == DateTime.DaysInMonth(maxDate.Year, maxDate.Month))
                {
                    fromDate = maxDate.AddDays(1);
                    var month = maxDate.Month == 12 ? 1 : maxDate.Month + 1;
                    var year = maxDate.Month == 12 ? maxDate.Year + 1 : maxDate.Year;

                    toDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }
                else
                {
                    fromDate = maxDate;
                    toDate = new DateTime(maxDate.Year, maxDate.Month,
                        DateTime.DaysInMonth(maxDate.Year, maxDate.Month));
                }


                //check prolongation - (if not set end date)
                //if not monthly invoice set to end date
                if (!isMonthlyInvoice ||
                    (!order.AutoProlongation && 
                     orderPosition.ToDate.Month == toDate.Month &&
                     orderPosition.ToDate.Year == toDate.Year))
                {
                    if (orderPosition.ToDate > toDate)
                    {
                        toDate = orderPosition.ToDate;
                    }
                }
            }
            else
            {
                if (isMonthlyInvoice && (orderPosition.ToDate.Month != DateTime.Now.Month ||
                    orderPosition.ToDate.Year != DateTime.Now.Year))
                {
                    var temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                        DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                    if (temp < fromDate)
                    {
                        toDate = new DateTime(fromDate.Year, fromDate.Month,
                            DateTime.DaysInMonth(fromDate.Year, fromDate.Month));
                    }
                    else
                    {
                        toDate = temp;
                    }
                }
            }
        }
    }
}
