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
    public partial class AddInvoicesController: InvoicesController
    {
        private readonly IUniqueNumberProvider numberProvider;
        private readonly IOrdersManager ordersManager;
        private readonly ITaxesManager taxesManager;
        private readonly IInvoicePositionsManager invoicePositionsManager;

        public AddInvoicesController(IInvoicesManager manager, IOrdersManager ordersManager,
            ITaxesManager taxesManager, IInvoicePositionsManager invoicePositionsManager, IUniqueNumberProvider numberProvider)
            : base(manager) 
        {
            this.numberProvider = numberProvider;
            this.ordersManager = ordersManager;
            this.taxesManager = taxesManager;
            this.invoicePositionsManager = invoicePositionsManager;
        }

        public override IHttpActionResult Post(InvoicesModel model)
        {
            var order = ordersManager.GetById(model.orderId);
            var taxValues = taxesManager.GetEntities(o => o.FromDate.Date <= DateTime.Now.Date && o.ToDate.Date >= DateTime.Now).ToList();
            double taxValue = 19;
            if(taxValues.Count != 0)
            {
                var minToDate = taxValues.Min(o => o.ToDate.Date);
                var temp = taxValues.FirstOrDefault(o => o.ToDate.Date == minToDate);
                if(temp != null)
                {
                    taxValue = temp.Value;
                }
            }

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
            Manager.AddEntity(invoice);

            foreach(var item in order.Positions.Where(o => !o.DeleteDate.HasValue))
            {
                var newPosition = new InvoicePositions()
                {
                    Positions = item,
                    Invoices = invoice,
                    Price = item.Price,
                    CreateDate = DateTime.Now,
                    ChangeDate = DateTime.Now,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate,
                };
                invoicePositionsManager.AddEntity(newPosition);
            }

            Manager.SaveChanges();

            model.Id = invoice.Id;
            return Ok(model);//Redirect(String.Format("Invoices/{0}", invoice.Id));
        }
    }
}
