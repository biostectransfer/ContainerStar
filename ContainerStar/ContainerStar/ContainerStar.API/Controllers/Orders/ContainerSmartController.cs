
using System;
using System.Collections.Generic;
using System.Linq;
using ContainerStar.API.Controllers.Settings;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;

namespace ContainerStar.API.Controllers
{
    public class ContainerSmartController : ContainersController
    {
        public ContainerSmartController(IContainersManager manager) : base(manager)
        {
        }

        protected override IQueryable<Containers> Filter(IQueryable<Containers> entities, Filtering filtering)
        {
            //Filter is performed by only 3 parameters : 1. container type, 2. available from, 3. available to
            DateTime? fromDateTemp;
            DateTime? toDateTemp;
            int? typeId;

            GetFilters(filtering, out fromDateTemp, out toDateTemp, out typeId);

            if (!fromDateTemp.HasValue || !toDateTemp.HasValue)
            {
                return null;
            }

            var positions = Manager.GetActualPositions(fromDateTemp.Value, toDateTemp.Value);
            var ids = GetActualIds(positions);

            return Manager.GetFreeContainers(ids, typeId);
        }

        private IEnumerable<int> GetActualIds(IEnumerable<Positions> positions)
        {
            var result = new List<int>();
            foreach (var position in positions)
            {
                if (!result.Contains(position.ContainerId.Value))
                {
                    result.Add(position.ContainerId.Value);
                }
            }
            return result;
        }

        private void GetFilters(Filtering filtering, out DateTime? fromDate, out DateTime? toDate, out int? typeId)
        {
            var fromDateTemp = DateTime.MinValue;
            var toDateTemp = DateTime.MinValue;
            var typeIdTemp = -1;
            foreach (var compositeFilter in filtering.Filters)
            {
                foreach (var filter in compositeFilter.Filters)
                {
                    switch (filter.Field.ToLower())
                    {
                        case "fromdate":
                            DateTime.TryParse(filter.Value, out fromDateTemp);
                            break;
                        case "todate":
                            DateTime.TryParse(filter.Value, out toDateTemp);
                            break;
                        case "containertypeid":
                            int.TryParse(filter.Value, out typeIdTemp);
                            break;
                        default:
                            break;

                    }
                }
            }
            fromDate = (fromDateTemp == DateTime.MinValue) ? (DateTime?)null : fromDateTemp;
            toDate = (toDateTemp == DateTime.MinValue) ? (DateTime?)null : toDateTemp;
            typeId = (typeIdTemp < 1) ? (int?)null : typeIdTemp;
        }
    }
}
