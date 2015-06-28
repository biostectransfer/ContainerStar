
using System;
using System.Collections.Generic;
using System.Linq;
using ContainerStar.Contracts.Entities;

namespace ContainerStar.Contracts.Managers
{
    public partial interface IContainersManager
    {
        List<Positions> GetActualPositions(DateTime dateFrom, DateTime dateTo);

        IQueryable<Containers> GetFreeContainers(IEnumerable<int> usedIds, int? typeId, string name, List<int> equipmentIds);
    }
}
