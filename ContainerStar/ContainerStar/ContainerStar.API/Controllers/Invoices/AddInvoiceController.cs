using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Exceptions;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ContainerStar.API.Controllers.Invoices
{
    public partial class AddInvoicesController: ApiController
    {
        private readonly IInvoicesManager invoicesManager;
        private readonly IUniqueNumberProvider numberProvider;
        private readonly IOrdersManager ordersManager;
        private readonly ITaxesManager taxesManager;
        private readonly IInvoicePositionsManager invoicePositionsManager;

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
            double taxValue = CalculateTaxes();

            var invoice = new ContainerStar.Contracts.Entities.Invoices()
            {
                InvoiceNumber = numberProvider.GetNextInvoiceNumber(),
                Orders = order,
                TaxValue = taxValue,
                WithTaxes = order.Customers.WithTaxes,
                Discount = order.Discount ?? 0,
                CreateDate = DateTime.Now,
                ChangeDate = DateTime.Now,
            };

            invoicesManager.AddEntity(invoice);

            AddInvoicePositions(order, invoice);

            invoicesManager.SaveChanges();

            model.Id = invoice.Id;
            return Ok(model);
        }

        private void AddInvoicePositions(Orders order, Contracts.Entities.Invoices invoice)
        {
            var orderPositions = order.Positions.Where(o => !o.DeleteDate.HasValue);

            var allInvoicePositions = invoicePositionsManager.GetEntities(o => o.Positions.OrderId == order.Id && !o.DeleteDate.HasValue).ToList();
            
            foreach (var orderPosition in orderPositions)
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
                    //TODO prolongation
                    amount = 1;

                    GetPeriod(orderPosition, invoicePositions, ref fromDate, ref toDate);
                }

                if (amount != 0)
                {
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
                    invoicePositionsManager.AddEntity(newPosition);
                }
            }
        }

        private void GetPeriod(Positions orderPosition, IEnumerable<InvoicePositions> invoicePositions, ref DateTime fromDate, ref DateTime toDate)
        {
            if (invoicePositions != null && invoicePositions.Count() != 0)
            {
                var maxDate = invoicePositions.Max(o => o.ToDate);

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
            }
            else
            {
                //if (orderPosition.FromDate.Month != DateTime.Now.Month ||
                //    orderPosition.FromDate.Year != DateTime.Now.Year)
                //{
                //    fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //}

                if (orderPosition.ToDate.Month != DateTime.Now.Month ||
                    orderPosition.ToDate.Year != DateTime.Now.Year)
                {
                    toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                        DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }
            }
        }

        private double CalculateTaxes()
        {
            var taxValues = taxesManager.GetEntities(o => o.FromDate.Date <= DateTime.Now.Date && o.ToDate.Date >= DateTime.Now).ToList();
            double taxValue = 19;
            if (taxValues.Count != 0)
            {
                var minToDate = taxValues.Min(o => o.ToDate.Date);
                var temp = taxValues.FirstOrDefault(o => o.ToDate.Date == minToDate);
                if (temp != null)
                {
                    taxValue = temp.Value;
                }
            }
            return taxValue;
        }
    }
}
