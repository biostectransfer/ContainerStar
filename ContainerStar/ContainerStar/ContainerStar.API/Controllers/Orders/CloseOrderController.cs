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
    ///     Controller for close order
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public partial class CloseOrderController : ApiController
    {
        private readonly IOrdersManager manager;

        public CloseOrderController(IOrdersManager manager)
        {
            this.manager = manager;
        }

        public IHttpActionResult Put(OrdersModel model)
        {
            var order = manager.GetById(model.Id);
            order.Status = (int)OrderStatusTypes.Closed;
            manager.SaveChanges();

            return Ok(new { id = model.Id });
        }
    }
}
