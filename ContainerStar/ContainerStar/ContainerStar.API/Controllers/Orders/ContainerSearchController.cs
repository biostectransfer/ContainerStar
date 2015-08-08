
using System;
using System.Collections.Generic;
using System.Linq;
using ContainerStar.API.Models.Orders;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Enums;

namespace ContainerStar.API.Controllers
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    public class ContainerSearchController : ClientApiController<ContainerSmartModel, Containers, int, IContainersManager>
    {
        private DateTime fromDate;
        private DateTime toDate;

        public ContainerSearchController(IContainersManager manager)
            : base(manager)
        {
        }

        protected override void EntityToModel(Containers entity, ContainerSmartModel model)
        {
            model.number = entity.Number;
            model.containerTypeId = entity.ContainerTypeId;
            model.length = entity.Length;
            model.width = entity.Width;
            model.height = entity.Height;
            model.color = entity.Color;
            model.price = entity.Price;
            model.proceedsAccount = entity.ProceedsAccount;
            model.isVirtual = entity.IsVirtual;
            model.manufactureDate = entity.ManufactureDate;
            model.boughtFrom = entity.BoughtFrom;
            model.boughtPrice = entity.BoughtPrice;
            model.comment = entity.Comment;
            model.sellPrice = entity.SellPrice;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;

            //specific
            model.fromDate = fromDate;
            model.toDate = toDate;
        }

        protected override void ModelToEntity(ContainerSmartModel model, Containers entity, ActionTypes actionType)
        {
            entity.Number = model.number;
            entity.ContainerTypeId = model.containerTypeId;
            entity.Length = model.length;
            entity.Width = model.width;
            entity.Height = model.height;
            entity.Color = model.color;
            entity.Price = model.price;
            entity.ProceedsAccount = model.proceedsAccount;
            entity.IsVirtual = model.isVirtual;
            entity.ManufactureDate = model.manufactureDate;
            entity.BoughtFrom = model.boughtFrom;
            entity.BoughtPrice = model.boughtPrice;
            entity.Comment = model.comment;
            entity.SellPrice = model.sellPrice;
        }

        protected override IQueryable<Containers> Filter(IQueryable<Containers> entities, Filtering filtering)
        {
            //Filter is performed by parameters : 
            //1. container type
            //2. available from, 3. available to
            //4. name (freetext)
            //5. equipments
            
            int? typeId;
            string name;
            List<int> equpmentIds;

            GetFilters(filtering, out typeId, out name, out equpmentIds);

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                return Manager.GetFreeContainers(new List<int>(), typeId, name, equpmentIds); //for offers we return containers independs from booking
            }

            var positions = Manager.GetActualPositions(fromDate, toDate);
            var ids = positions.Select(o => o.ContainerId.Value).Distinct().ToList();

            return Manager.GetFreeContainers(ids, typeId, name, equpmentIds);
        }
        
        private void GetFilters(Filtering filtering, out int? typeId, out string name, out List<int> equipmentIds)
        {
            fromDate = DateTime.MinValue;
            toDate = DateTime.MinValue;
            var typeIdTemp = -1;
            name = String.Empty;
            equipmentIds = new List<int>();

            foreach (var compositeFilter in filtering.Filters)
            {
                foreach (var filter in compositeFilter.Filters)
                {
                    switch (filter.Field.ToLower())
                    {
                        case "fromdate":
                            DateTime.TryParse(filter.Value, out fromDate);
                            break;
                        case "todate":
                            DateTime.TryParse(filter.Value, out toDate);
                            break;
                        case "containertypeid":
                            int.TryParse(filter.Value, out typeIdTemp);
                            break;
                        case "name":
                            name = filter.Value;
                            break;
                        case "equipments":
                            if (!String.IsNullOrEmpty(filter.Value))
                            {
                                var parts = filter.Value.Split(',');
                                foreach (var part in parts)
                                {
                                    int temp;
                                    if (Int32.TryParse(part, out temp))
                                    {
                                        equipmentIds.Add(temp);
                                    }
                                }
                            }
                            break;
                        default:
                            break;

                    }
                }
            }
            typeId = (typeIdTemp < 1) ? (int?)null : typeIdTemp;
        }        
    }
}
