using ContainerStar.API.ActionResults;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;

namespace ContainerStar.API.Controllers
{
    
    /// <summary>
    ///     Controller for printing
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public partial class PrintController: ApiController
    {
        private ITaxesManager taxesManager;
        private IInvoicesManager invoicesManager;
        private IInvoiceStornosManager invoiceStornosManager;
        private ITransportOrdersManager transportOrdersManager;

        public PrintController(IOrdersManager manager, IInvoicesManager invoicesManager, 
            IInvoiceStornosManager invoiceStornosManager, ITaxesManager taxesManager,
            ITransportOrdersManager transportOrdersManager) :
            base()
        {
            this.taxesManager = taxesManager;
            this.invoicesManager = invoicesManager;
            this.invoiceStornosManager = invoiceStornosManager;
            this.transportOrdersManager = transportOrdersManager;
            Manager = manager;
            FilterExpressionCreator = new FilterExpressionCreator();
        }

        private IOrdersManager Manager { get; set; }
        private IFilterExpressionCreator FilterExpressionCreator { get; set; }

        public IHttpActionResult Get([FromUri]GridArgs args, int id, int printTypeId)
		{
            string path = String.Empty;
            var dataDirectory = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
            var report = (PrintTypes)printTypeId;
            MemoryStream stream = null;

            switch (report)
            {
                case PrintTypes.RentOrder:
                    path = Path.Combine(dataDirectory, API.Configuration.RentOrderFileName);
                    stream = Manager.PrepareRentOrderPrintData(id, path, taxesManager);
                    break;
                case PrintTypes.Offer:
                    path = Path.Combine(dataDirectory, API.Configuration.OfferFileName);
                    stream = Manager.PrepareOfferPrintData(id, path, taxesManager);
                    break;
                case PrintTypes.Invoice:
                    path = Path.Combine(dataDirectory, API.Configuration.InvoiceFileName);
                    stream = Manager.PrepareInvoicePrintData(id, path, invoicesManager);
                    break;
                case PrintTypes.ReminderMail:
                    path = Path.Combine(dataDirectory, API.Configuration.ReminderFileName);

                    var invoices = invoicesManager.GetEntities(o => !o.PayDate.HasValue &&
                        ( (!o.LastReminderDate.HasValue && o.CreateDate.AddDays(o.PayInDays) < DateTime.Now) ||
                          (o.LastReminderDate.HasValue && o.LastReminderDate.Value.AddDays(8) < DateTime.Now)
                        )).ToList();

                    foreach(var invoice in invoices)
                    {
                        invoice.LastReminderDate = DateTime.Now;
                        invoice.ReminderCount++;
                    }

                    invoicesManager.SaveChanges();

                    var newIds = invoices.Select(o => o.Id).ToList();

                    var allInvoicesToReminder = new List<Contracts.Entities.Invoices>(invoices);
                    allInvoicesToReminder.AddRange(invoicesManager.GetEntities(o => !o.PayDate.HasValue && o.ReminderCount != 0 &&
                        !newIds.Contains(o.Id)).ToList());

                    stream = Manager.PrepareReminderPrintData(allInvoicesToReminder, path, invoicesManager, taxesManager);
                    break;
                case PrintTypes.InvoiceStorno:
                    path = Path.Combine(dataDirectory, API.Configuration.InvoiceStornoFileName);
                    stream = Manager.PrepareInvoiceStornoPrintData(id, path, invoiceStornosManager);
                    break;
                case PrintTypes.TransportInvoice:
                    path = Path.Combine(dataDirectory, API.Configuration.TransportInvoiceFileName);
                    stream = Manager.PrepareTransportInvoicePrintData(id, path, transportOrdersManager, taxesManager);
                    break;
                default:
                    throw new NotImplementedException();
            }

            var result = new StreamActionResult(new MemoryStream(stream.ToArray()));
            result.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            result.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = Path.GetFileName(path) };

            return result;
		}
    }
}
