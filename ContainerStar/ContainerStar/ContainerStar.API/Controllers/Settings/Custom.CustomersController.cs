using ContainerStar.API.Models.Settings;
using ContainerStar.Contracts.Entities;
using System.Web.Http;
using CoreBase;
using System.Collections.Generic;
using System;

namespace ContainerStar.API.Controllers.Settings
{
    public partial class CustomersController
    {
        protected override string BuildWhereClause<T>(Filter filter)
        {
            if (filter.Field == "name")
            {
                var clauses = new List<string>();
                int number = 0;
                if (!String.IsNullOrEmpty(filter.Value))
                {
                    Int32.TryParse(filter.Value, out number);
                }

                clauses.AddRange(new[] {
                        base.BuildWhereClause<T>(new Filter { Field = "Name", Operator = filter.Operator, Value = filter.Value }),
                        base.BuildWhereClause<T>(new Filter { Field = "Street", Operator = filter.Operator, Value = filter.Value })
                    });

                return String.Format("{0}{1}", String.Join(" or ", clauses),
                    number != 0 ? String.Format(" or {0} ", number) : String.Empty);
            }
            else if (filter.Field == "isProspectiveCustomer")
            {
                bool isProspectiveCustomer;
                if (!String.IsNullOrEmpty(filter.Value))
                {
                    Boolean.TryParse(filter.Value, out isProspectiveCustomer);

                    return String.Format("IsProspectiveCustomer == {0}", isProspectiveCustomer);
                }
            }

            return base.BuildWhereClause<T>(filter);
        }
    }
}
