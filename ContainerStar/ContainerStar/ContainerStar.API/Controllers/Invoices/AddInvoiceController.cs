using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Exceptions;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ContainerStar.API.Controllers.Invoices
{
    public partial class AddInvoicesController: InvoicesController
    {
        public AddInvoicesController(IInvoicesManager manager) : base(manager) { }

        public override IHttpActionResult Get([FromUri] int id)
        {
            var model = new InvoicesModel();
            model.orderId = id;

            return Ok(model);
        }
    }
}
