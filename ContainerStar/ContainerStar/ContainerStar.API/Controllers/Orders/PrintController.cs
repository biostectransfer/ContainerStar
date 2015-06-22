using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace ContainerStar.API.Controllers
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    /// <summary>
    ///     Controller for printing
    /// </summary>
    public partial class PrintController: ApiController
    {
        public PrintController(IOrdersManager manager) :
            base()
        {
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
                    path = Path.Combine(dataDirectory, ContainerStar.API.Configuration.RentOrderFileName);
                    stream = Manager.PrepareRentOrderPrintData(id, path);
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
