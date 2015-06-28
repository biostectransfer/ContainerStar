using ContainerStar.API.Models.Settings;
using ContainerStar.Contracts.Entities;
using System.Web.Http;
using CoreBase;
using System.Collections.Generic;
using ContainerStar.Contracts.Managers;

namespace ContainerStar.API.Controllers.Settings
{
    public partial class ContainersController
    {
        protected override string BuildWhereClause<T>(Filter filter)
        {
            if (filter.Field == "name")
            {
                var clauses = new List<string>();

                clauses.AddRange(new[] { 
        				base.BuildWhereClause<T>(new Filter { Field = "Number", Operator = filter.Operator, Value = filter.Value }),
        				base.BuildWhereClause<T>(new Filter { Field = "ContainerTypes.Name", Operator = filter.Operator, 
                            Value = filter.Value }),
        			});

                return string.Join(" or ", clauses);
            }

            return base.BuildWhereClause<T>(filter);
        }

        protected void ExtraModelToEntity(Containers entity, ContainersModel model, ActionTypes actionType)
        {
            if (actionType == ActionTypes.Add)
            {
                entity.ContainerEquipmentRsps = new List<ContainerEquipmentRsp>();
                var containerTypeManager = GlobalConfiguration.Configuration.DependencyResolver.GetService<IContainerTypesManager>();
                var containerType = containerTypeManager.GetById(model.containerTypeId);
                foreach (var equipment in containerType.ContainerTypeEquipmentRsps)
                {
                    entity.ContainerEquipmentRsps.Add(new ContainerEquipmentRsp()
                    {
                        Amount = equipment.Amount,
                        Containers = entity,
                        EquipmentId = equipment.EquipmentId
                    });
                }
            }
        }
    }
}
