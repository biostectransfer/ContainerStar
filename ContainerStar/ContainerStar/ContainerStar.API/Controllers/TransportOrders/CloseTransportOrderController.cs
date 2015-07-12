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
    ///     Controller for close Transport order
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.TransportOrders })]
    public partial class CloseTransportOrderController : ApiController
    {
        private readonly ITransportOrdersManager manager;

        public CloseTransportOrderController(ITransportOrdersManager manager)
        {
            this.manager = manager;
        }

        public IHttpActionResult Put(TransportOrdersModel model)
        {
            var transportOrder = manager.GetById(model.Id);
            transportOrder.Status = (int)OrderStatusTypes.Closed;
            manager.SaveChanges();

            return Ok(new { id = model.Id });
        }
    }
}
