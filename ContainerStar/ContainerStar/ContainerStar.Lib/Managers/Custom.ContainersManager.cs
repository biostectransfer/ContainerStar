
using System;
using System.Collections.Generic;
using System.Linq;
using ContainerStar.Contracts.Entities;

namespace ContainerStar.Lib.Managers
{
    public partial class ContainersManager
    {
        public IQueryable<Positions> GetActualPositions(DateTime dateFrom, DateTime dateTo)
        {
            return DataContext.GetSet<Positions>()
                .Where(r => 
                    !r.DeleteDate.HasValue &&
                    r.ContainerId.HasValue && 
                    (!r.IsSellOrder || r.Containers.IsVirtual) && 
                    !r.Orders.IsOffer
                )
                .Where(r =>
                    (r.FromDate >= dateFrom && r.FromDate <= dateTo) || //from date inside period
                    (r.ToDate >= dateFrom && r.ToDate <= dateTo) || // to date inside period
                    (r.FromDate <= dateFrom && r.ToDate >= dateTo)).AsQueryable(); //period is a part of an existing one
        }

        public IQueryable<Containers> GetFreeContainers(IEnumerable<int> usedIds, int? containerTypeId, string name, List<int> equipmentIds)
        {
            if (equipmentIds == null || equipmentIds.Count == 0)
            {
                return DataContext.GetSet<Containers>()
                    .Where(r => !r.IsSold && !usedIds.Contains(r.Id))
                    .Where(r => (!containerTypeId.HasValue || r.ContainerTypeId == containerTypeId.Value))
                    .Where(r => (String.IsNullOrEmpty(name) || r.Number.ToLower().Contains(name.ToLower())));
            }
            else
            {
                return DataContext.GetSet<Containers>()
                    .Where(r => !usedIds.Contains(r.Id))
                    .Where(r => (!containerTypeId.HasValue || r.ContainerTypeId == containerTypeId.Value))
                    .Where(r => (String.IsNullOrEmpty(name) || r.Number.ToLower().Contains(name.ToLower()))).ToList()
                    .Where(r => equipmentIds.All(o => r.ContainerEquipmentRsps.Select(t => t.EquipmentId).Contains(o))).AsQueryable();
            }
        }
        
        public List<Positions> GetRentPositions(DateTime dateFrom, DateTime dateTo, int? containerTypeId, string name, IEnumerable<int> equipmentIds)
        {
            if (equipmentIds == null || equipmentIds.Count() == 0)
            {
                return DataContext.GetSet<Positions>()
                    .Where(r =>
                        !r.DeleteDate.HasValue &&
                        r.ContainerId.HasValue &&
                        (!r.IsSellOrder || r.Containers.IsVirtual) &&
                        !r.Orders.IsOffer)
                    .Where(r => (!containerTypeId.HasValue || r.Containers.ContainerTypeId == containerTypeId.Value))
                    .Where(r => (String.IsNullOrEmpty(name) || r.Containers.Number.ToLower().Contains(name.ToLower())))
                    .Where(r =>
                        (r.FromDate >= dateFrom && r.FromDate <= dateTo) || //from date inside period
                        (r.ToDate >= dateFrom && r.ToDate <= dateTo) || // to date inside period
                        (r.FromDate <= dateFrom && r.ToDate >= dateTo)).ToList(); //period is a part of an existing one
            }
            else
            {
                return DataContext.GetSet<Positions>()
                    .Where(r =>
                        !r.DeleteDate.HasValue &&
                        r.ContainerId.HasValue &&
                        (!r.IsSellOrder || r.Containers.IsVirtual) &&
                        !r.Orders.IsOffer)
                    .Where(r => (!containerTypeId.HasValue || r.Containers.ContainerTypeId == containerTypeId.Value))
                    .Where(r => (String.IsNullOrEmpty(name) || r.Containers.Number.ToLower().Contains(name.ToLower())))
                    .Where(r =>
                        (r.FromDate >= dateFrom && r.FromDate <= dateTo) || //from date inside period
                        (r.ToDate >= dateFrom && r.ToDate <= dateTo) || // to date inside period
                        (r.FromDate <= dateFrom && r.ToDate >= dateTo)).ToList() //period is a part of an existing one
                    .Where(r => equipmentIds.All(o => r.Containers.ContainerEquipmentRsps.Select(t => t.EquipmentId).Contains(o))).ToList();
            }
        }
    }
}
