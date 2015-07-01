using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using CoreBase;
using System;
using System.Web.Http;

namespace ContainerStar.API.Controllers
{

    /// <summary>
    ///     Controller for offers
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Invoices })]
    public partial class PayBillController : ApiController
    {
        private readonly IInvoicesManager manager;

        public PayBillController(IInvoicesManager manager)
        {
            this.manager = manager;
        }

        public IHttpActionResult Put(InvoicesModel model)
        {
            var invoice = manager.GetById(model.Id);
            invoice.PayDate = DateTime.Now;
            manager.SaveChanges();

            return Ok(new { id = model.Id });
        }
    }
}
