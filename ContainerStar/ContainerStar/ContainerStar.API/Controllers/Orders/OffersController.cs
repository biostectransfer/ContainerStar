using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Http;

namespace ContainerStar.API.Controllers
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    /// <summary>
    ///     Controller for offers
    /// </summary>
    public partial class OffersController: ApiController
    {
        protected IOrdersManager Manager { get; private set; }
        
        public OffersController(IOrdersManager manager)
        {
            Manager = manager;
        }

        public IHttpActionResult Put(OrdersModel model)
        {
            var order = Manager.GetById(model.Id);
            order.IsOffer = false;
            Manager.SaveChanges();

            return Ok(new { id = model.Id });
        }
    }
}
