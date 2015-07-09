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

        public IHttpActionResult Post(int containerTypeId, string name)
        {
            return Ok(new [] { 
            
                    new {
                        title = "All Day Event",
                        start = "2015-02-01"
                    },
                    new {
                        title = "Long Event",
                        start = "2015-02-07",
                        //end = "2015-02-10"
                    },
                    new {
                        //id =  999,
                        title = "Repeating Event",
                        start = "2015-02-09T16:00:00"
                    },
                    new {
                        //id = 999,
                        title = "Repeating Event",
                        start = "2015-02-16T16:00:00"
                    },
                    new {
                        title = "Conference",
                        start = "2015-02-11",
                        //end = "2015-02-13"
                    },
                    new {
                        title = "Meeting",
                        start = "2015-02-12T10:30:00",
                        //end = "2015-02-12T12:30:00"
                    },
                    new {
                        title = "Lunch",
                        start = "2015-02-12T12:00:00"
                    },
                    new {
                        title = "Meeting",
                        start = "2015-02-12T14:30:00"
                    },
                    new {
                        title = "Happy Hour",
                        start = "2015-02-12T17:30:00"
                    },
                    new {
                        title = "Dinner",
                        start = "2015-02-12T20:00:00"
                    },
                    new {
                        title = "Birthday Party",
                        start = "2015-02-13T07:00:00"
                    },
                    new {
                        title = "Click for Google",
                        //url = "http://google.com/",
                        start = "2015-02-28"
                    },
                    new {
                        title = String.Format("{0} {1}", containerTypeId, name),
                        start = "2015-07-09T07:00:00"
                    },
            });
        }
    }
}
