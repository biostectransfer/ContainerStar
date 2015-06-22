using ContainerStar.API.Models.Settings;
using ContainerStar.Contracts.Entities;
using System.Web.Http;
using CoreBase;
using System.Collections.Generic;

namespace ContainerStar.API.Controllers.Settings
{
    public partial class ContainerTypesController
    {
        protected override string BuildWhereClause<T>(Filter filter)
        {
            if (filter.Field == "name")
            {
                var clauses = new List<string>();

                clauses.AddRange(new[] { 
        				base.BuildWhereClause<T>(new Filter { Field = "Name", Operator = filter.Operator, Value = filter.Value }),
        				base.BuildWhereClause<T>(new Filter { Field = "Comment", Operator = filter.Operator, 
                            Value = filter.Value }),
        			});

                return string.Join(" or ", clauses);
            }

            return base.BuildWhereClause<T>(filter);
        }
    }
}
