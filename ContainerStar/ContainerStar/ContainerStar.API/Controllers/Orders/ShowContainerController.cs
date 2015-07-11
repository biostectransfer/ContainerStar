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
        public bool SearchFreeContainer { get; set; }

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
    public partial class ShowContainerController : ApiController
    {
        private readonly IContainersManager manager;
        private readonly IUniqueNumberProvider numberProvider;

        public ShowContainerController(IContainersManager manager, IUniqueNumberProvider numberProvider)
        {
            this.manager = manager;
            this.numberProvider = numberProvider;
        }

        public IHttpActionResult Post(ContainerSearchModel model)
        {
            var result = new List<ContainerViewModel>();

            if (!String.IsNullOrEmpty(model.StartDateStr) && !String.IsNullOrEmpty(model.EndDateStr))
            {
                if (model.SearchFreeContainer)
                {
                    var positionsQuery = manager.GetActualPositions(model.StartDate, model.EndDate).
                           Where(r => model.ContainerTypeId == 0 || r.Containers.ContainerTypeId == model.ContainerTypeId).
                           Where(r => String.IsNullOrEmpty(model.Name) || r.Containers.Number.ToLower().Contains(model.Name.ToLower()));

                    if (model.Equipments != null && model.Equipments.Count() != 0)
                    {
                        positionsQuery = positionsQuery.ToList()
                            .Where(r => model.Equipments.All(o => r.Containers.ContainerEquipmentRsps.Select(t => t.EquipmentId).Contains(o))).AsQueryable();
                    }

                    var positions = positionsQuery.ToList();
                    
                    //process rented containers in case if they are available at some days
                    foreach (var container in positions.Select(o => o.Containers).Where(o => !o.IsSold).Distinct())
                    {
                        DateTime? startDate = null;
                        DateTime? endDate = null;
                        for (var date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
                        {
                            if (positions.Where(o => o.ContainerId == container.Id).All(p => p.FromDate > date || p.ToDate < date))
                            {
                                if (startDate.HasValue)
                                {
                                    endDate = date;
                                }
                                else
                                {
                                    startDate = date;
                                    endDate = date;
                                }
                            }
                            else if (startDate.HasValue)
                            {
                                result.Add(new ContainerViewModel()
                                {
                                    start = startDate.Value.ToString("yyyy-MM-dd"),
                                    end = endDate.Value.AddDays(1).ToString("yyyy-MM-dd"), //Add 1 days because calender end date is not included
                                    url = String.Empty,
                                    title = String.Format("{0} {1}", container.Number, container.ContainerTypes.Name)
                                });

                                startDate = null;
                                endDate = null;
                            }
                        }
                        
                        if (startDate.HasValue)
                        {
                            result.Add(new ContainerViewModel()
                            {
                                start = startDate.Value.ToString("yyyy-MM-dd"),
                                end = endDate.Value.AddDays(1).ToString("yyyy-MM-dd"), //Add 1 days because calender end date is not included
                                url = String.Empty,
                                title = String.Format("{0} {1}", container.Number, container.ContainerTypes.Name)
                            });
                        }
                    }

                    AddFreeContainers(model, result, positions);
                }
                else
                {
                    GetRentContainers(model, result);
                }
            }

            return Ok(result);
        }

        private void AddFreeContainers(ContainerSearchModel model, List<ContainerViewModel> result, List<Contracts.Entities.Positions> positions)
        {
            var ids = positions.Select(o => o.ContainerId.Value).Distinct();
            var freeContainers = manager.GetEntities(o => !o.IsSold && !ids.Contains(o.Id) &&
                (model.ContainerTypeId == 0 || o.ContainerTypeId == model.ContainerTypeId) &&
                (String.IsNullOrEmpty(model.Name) || o.Number.ToLower().Contains(model.Name.ToLower())));

            if (model.Equipments != null && model.Equipments.Count() != 0)
            {
                freeContainers = freeContainers.ToList()
                    .Where(r => model.Equipments.All(o => r.ContainerEquipmentRsps.Select(t => t.EquipmentId).Contains(o)));
            }

            foreach (var container in freeContainers.ToList())
            {
                result.Add(new ContainerViewModel()
                {
                    start = model.StartDate.ToString("yyyy-MM-dd"),
                    end = model.EndDate.AddDays(1).ToString("yyyy-MM-dd"), //Add 1 days because calender end date is not included
                    url = String.Empty,
                    title = String.Format("{0} {1}", container.Number, container.ContainerTypes.Name)
                });
            }
        }

        private void GetRentContainers(ContainerSearchModel model, List<ContainerViewModel> result)
        {
            var positions = manager.GetRentPositions(model.StartDate, model.EndDate,
                model.ContainerTypeId != 0 ? model.ContainerTypeId : (int?)null, model.Name, model.Equipments);
            
            foreach (var position in positions)
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
    }
}
