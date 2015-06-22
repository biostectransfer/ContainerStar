using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders.Providers;

namespace ContainerStar.API.Controllers
{
    public class GridArgsModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof (GridArgs))
            {
                return false;
            }

            var queryString = HttpContext.Current.Request.QueryString;
            var provider = new QueryStringValueProvider(actionContext, CultureInfo.CurrentCulture);

            var result = new GridArgs
            {
                Filtering = new Filtering
                {
                    Filters = new List<CompositeFilter>()
                },
                Sorting = new Sorting(),
                Paging = new Paging()
            };

            var filterIndex = 0;
            var simpleFilterField = "filter[filters][{0}][field]";
            var simpleFilterOperator = "filter[filters][{0}][operator]";
            var simpleFilterValue = "filter[filters][{0}][value]";
            var complexFilterOperator = "filter[filters][{0}][logic]";
            var complexFilterField1 = "filter[filters][{0}][filters][0][field]";
            var complexFilterOperator1 = "filter[filters][{0}][filters][0][operator]";
            var complexFilterValue1 = "filter[filters][{0}][filters][0][value]";
            var complexFilterField2 = "filter[filters][{0}][filters][1][field]";
            var complexFilterOperator2 = "filter[filters][{0}][filters][1][operator]";
            var complexFilterValue2 = "filter[filters][{0}][filters][1][value]";
            var filterOperator = "filter[logic]";

            result.Paging.Take = Convert.ToInt32(queryString.Get("take"));
            result.Paging.Skip = Convert.ToInt32(queryString.Get("skip"));
            result.Sorting.Field = queryString.Get("sort[0][field]");
            result.Sorting.Direction = queryString.Get("sort[0][dir]");

            result.Filtering.Logic = queryString.Get(filterOperator);

            while (filterIndex < 20)
            {
                if (queryString.Get(string.Format(simpleFilterField, filterIndex)) != null)
                {
                    var compositeFilter = new CompositeFilter
                    {
                        Filters = new List<Filter>(),
                        Logic = "and"
                    };

                    compositeFilter.Filters.Add(new Filter
                    {
                        Field = queryString.Get(string.Format(simpleFilterField, filterIndex)),
                        Value = queryString.Get(string.Format(simpleFilterValue, filterIndex)),
                        Operator = queryString.Get(string.Format(simpleFilterOperator, filterIndex))
                    });

                    result.Filtering.Filters.Add(compositeFilter);
                }
                else if (queryString.Get(string.Format(complexFilterOperator, filterIndex)) != null)
                {
                    var filtering = new CompositeFilter
                    {
                        Filters = new List<Filter>(),
                        Logic = queryString.Get(string.Format(complexFilterOperator, filterIndex))
                    };

                    filtering.Filters.Add(new Filter
                    {
                        Field = queryString.Get(string.Format(complexFilterField1, filterIndex)),
                        Value = queryString.Get(string.Format(complexFilterValue1, filterIndex)),
                        Operator = queryString.Get(string.Format(complexFilterOperator1, filterIndex))
                    });

                    filtering.Filters.Add(new Filter
                    {
                        Field = queryString.Get(string.Format(complexFilterField2, filterIndex)),
                        Value = queryString.Get(string.Format(complexFilterValue2, filterIndex)),
                        Operator = queryString.Get(string.Format(complexFilterOperator2, filterIndex))
                    });

                    result.Filtering.Filters.Add(filtering);
                }
                else
                {
                    break;
                }

                filterIndex++;
            }

            bindingContext.Model = result;

            return true;
        }
    }
}