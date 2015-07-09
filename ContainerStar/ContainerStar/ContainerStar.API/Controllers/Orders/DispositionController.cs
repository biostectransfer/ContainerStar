using ContainerStar.API.Models;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using CoreBase;
using System;
using System.Web.Http;

namespace ContainerStar.API.Controllers
{
    public class Test
    {
        public int containerTypeId { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    /// <summary>
    ///     Controller for offers
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public partial class DispositionController : ApiController
    {
        private readonly IOrdersManager manager;
        private readonly IUniqueNumberProvider numberProvider;

        public DispositionController(IOrdersManager manager, IUniqueNumberProvider numberProvider)
        {
            this.manager = manager;
            this.numberProvider = numberProvider;
        }

        public IHttpActionResult Post(Test model)
        {
            return Ok(new [] {            
                    
                    new {
                        title = "Click for Google",
                        url = "#Orders/1005",
                        start = "2015-07-18T09:00:00",
                        end = String.Empty,
                    },
                    new {
                        title = String.Format("{0} {1} {2} {3}", model.containerTypeId, model.name,  model.startDate,  model.endDate),
                        url = String.Empty,
                        start = "2015-07-09T07:00:00",
                        end = "2015-07-15T09:00:00",
                    },
            });
        }
    }
}
