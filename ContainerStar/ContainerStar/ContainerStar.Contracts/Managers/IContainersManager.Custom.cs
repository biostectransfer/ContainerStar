
using System;
using System.Collections.Generic;
using System.Linq;
using ContainerStar.Contracts.Entities;

namespace ContainerStar.Contracts.Managers
{
    public partial interface IContainersManager
    {
        List<Positions> GetActualPositions(DateTime dateFrom, DateTime dateTo);

        IQueryable<Containers> GetFreeContainers(IEnumerable<int> usedIds, int? containerTypeId, string name, List<int> equipmentIds);

        List<Positions> GetRentPositions(DateTime dateFrom, DateTime dateTo, int? containerTypeId, string name, IEnumerable<int> equipmentIds);
    }
}
