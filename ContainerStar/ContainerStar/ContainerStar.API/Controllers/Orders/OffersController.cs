using ContainerStar.API.Models;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using CoreBase;
using System;
using System.Web.Http;
using System.Linq;

namespace ContainerStar.API.Controllers
{

    /// <summary>
    ///     Controller for offers
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public partial class OffersController : ApiController
    {
        private readonly IOrdersManager manager;
        private readonly ICustomersManager customerManager;
        private readonly IUniqueNumberProvider numberProvider;

        public OffersController(IOrdersManager manager, ICustomersManager customerManager, IUniqueNumberProvider numberProvider)
        {
            this.manager = manager;
            this.customerManager = customerManager;
            this.numberProvider = numberProvider;
        }

        public IHttpActionResult Put(OrdersModel model)
        {
            var order = manager.GetById(model.Id);
            order.IsOffer = false;
            
            if (String.IsNullOrEmpty(order.OrderNumber))
            {
                order.OrderNumber = numberProvider.GetNextOrderNumber();
            }

            if (String.IsNullOrEmpty(order.RentOrderNumber))
            {
                order.RentOrderNumber = numberProvider.GetNextRentOrderNumber(Contracts.Configuration.RentOrderPreffix);
            }

            if(order.Customers.IsProspectiveCustomer)
            {
                order.Customers.IsProspectiveCustomer = false;
                var lastCustomerNumber = customerManager.GetEntities().Max(o => o.Number);
                order.Customers.Number = lastCustomerNumber + 1;
            }

            manager.SaveChanges();

            return Ok(new { id = model.Id });
        }
    }
}
