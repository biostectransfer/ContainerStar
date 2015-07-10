using ContainerStar.API.Models;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using CoreBase;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace ContainerStar.API.Controllers
{
    public class ContainerSearchModel
    {
        public int ContainerTypeId { get; set; }
        public string Name { get; set; }
        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }
        public IEnumerable<int> Equipments { get; set; }

        public DateTime StartDate
        {
            get
            {
                var result = DateTime.Now;
                DateTime.TryParse(StartDateStr, out result);
                return result;
            }
        }

        public DateTime EndDate
        {
            get
            {
                var result = DateTime.Now;
                DateTime.TryParse(EndDateStr, out result);
                return result;
            }
        }
    }

    public class ContainerViewModel
    {
        public string title { get; set; }
        public string url { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }

    /// <summary>
    ///     Controller for Disposition
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public partial class ShowRentContainerController : ApiController
    {
        private readonly IContainersManager manager;
        private readonly IUniqueNumberProvider numberProvider;

        public ShowRentContainerController(IContainersManager manager, IUniqueNumberProvider numberProvider)
        {
            this.manager = manager;
            this.numberProvider = numberProvider;
        }

        public IHttpActionResult Post(ContainerSearchModel model)
        {
            var result = new List<ContainerViewModel>();

            if (!String.IsNullOrEmpty(model.StartDateStr) && !String.IsNullOrEmpty(model.EndDateStr))
            {
                var positions = manager.GetRentPositions(model.StartDate, model.EndDate, 
                    model.ContainerTypeId != 0 ? model.ContainerTypeId : (int?)null, model.Name, model.Equipments);
                
                //var ids = positions.Select(o => o.ContainerId.Value).Distinct();

                foreach(var position in positions)
                {
                    result.Add(new ContainerViewModel()
                    {
                        start = position.FromDate.ToString("yyyy-MM-dd"),
                        end = position.ToDate.AddDays(1).ToString("yyyy-MM-dd"), //Add 1 days because calender end date is not included
                        url = String.Format("#Orders/{0}", position.OrderId),
                        title = String.Format("{0} {1} ({2})", position.Containers.Number,
                            position.Containers.ContainerTypes.Name, position.Orders.RentOrderNumber)
                    });
                }
            }

            return Ok(result);
        }
    }
}
