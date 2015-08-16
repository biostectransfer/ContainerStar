using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using System.Web.Http.Dependencies;
using CoreBase.Controllers;
using CoreBase.Entities;

namespace ContainerStar.API.Controllers
{
    public partial class CollectionTypesModel
    {
        public bool CommunicationPartners { get; set; }
        public bool PaymentIntervals { get; set; }
        public bool PaymentTypes { get; set; }
        public bool ContainerTypesForDisposition { get; set; }
        public bool ProceedsAccounts { get; set; }
    }
    
    public class ViewCollectionController: ApiController
    {
        public ViewCollectionController()
        {
        }

        [Authorize]
        public IHttpActionResult Get([FromUri] CollectionTypesModel model)
        {
            if (model == null)
                return NotFound();

            var result = new Dictionary<string, IEnumerable<object>>();

            if (model.CommunicationPartners)
            {
                var manager = (ICommunicationPartnersManager)GlobalConfiguration.Configuration.DependencyResolver.
                    GetService(typeof(ICommunicationPartnersManager));

                result.Add("CommunicationPartners", manager.GetEntities().
                    Select(o => new { id = o.Id, name = o.Title, customerId = o.CustomerId }).ToList());
            }

            if (model.PaymentIntervals)
                result.Add("PaymentIntervals", new[]
                {
                    new { id = 0, name = "Sofort"},
                    new { id = 5, name = "5 Tage"},
                    new { id = 10, name = "10 Tage"},
                    new { id = 30, name = "30 Tage"},
                });

            if (model.PaymentTypes)
                result.Add("PaymentTypes", new[]
                {
                    new { id = 0, name = "Monat"},
                    new { id = 1, name = "Tag"},
                    new { id = 2, name = "Pauschal"},
                });


            if (model.ContainerTypesForDisposition)
            {
                var manager = (IContainerTypesManager)GlobalConfiguration.Configuration.DependencyResolver.
                    GetService(typeof(IContainerTypesManager));
                var containerTypes = manager.GetEntities().Where(o => o.DispositionRelevant && !o.DeleteDate.HasValue).ToList();

                result.Add("ContainerTypesForDisposition", containerTypes.Cast<IHasTitle<int>>().OrderBy(o => o.EntityTitle)
                         .Select(o => new IdNameModel<int> { id = o.Id, name = o.EntityTitle }));
            }

            if (model.ProceedsAccounts)
            {
                var proceedsAccounts = new List<int>();

                var containersManager = (IContainersManager)GlobalConfiguration.Configuration.DependencyResolver.
                    GetService(typeof(IContainersManager));
                proceedsAccounts.AddRange(containersManager.GetEntities().Select(o => o.ProceedsAccount).Distinct());

                var additionalCostsManager = (IAdditionalCostsManager)GlobalConfiguration.Configuration.DependencyResolver.
                    GetService(typeof(IAdditionalCostsManager));
                proceedsAccounts.AddRange(additionalCostsManager.GetEntities().Select(o => o.ProceedsAccount).Distinct());

                result.Add("ProceedsAccounts", proceedsAccounts.OrderBy(o => o)
                         .Select(o => new IdNameModel<int> { id = o, name = o.ToString() }));
            }

            new MasterDataViewCollectionControllerFactory().GetViewCollections(
                GlobalConfiguration.Configuration.DependencyResolver, model, result);
                       			
			return Ok(result);
        }
    }
}
