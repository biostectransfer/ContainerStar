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

        public PrintController(IOrdersManager manager, IInvoicesManager invoicesManager, ITaxesManager taxesManager) :
            base()
        {
            this.taxesManager = taxesManager;
            this.invoicesManager = invoicesManager;
            Manager = manager;
            FilterExpressionCreator = new FilterExpressionCreator();
        }

        private IOrdersManager Manager { get; set; }
        private IFilterExpressionCreator FilterExpressionCreator { get; set; }

        public HttpResponseMessage Get([FromUri]GridArgs args, int id, int printTypeId)
		{
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            
            string path = String.Empty;
            var dataDirectory = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
            var report = (PrintTypes)printTypeId;
            Stream stream = null;

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
                    stream = Manager.PrepareInvoicePrintData(id, path, invoicesManager, taxesManager);
                    break;
                case PrintTypes.ReminderMail:
                    path = Path.Combine(dataDirectory, API.Configuration.ReminderFileName);
                    stream = Manager.PrepareReminderPrintData(id, path, invoicesManager, taxesManager);
                    break;
                default:
                    throw new NotImplementedException();
            }
            
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = 
                new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

            return response;
		}
    }
}
