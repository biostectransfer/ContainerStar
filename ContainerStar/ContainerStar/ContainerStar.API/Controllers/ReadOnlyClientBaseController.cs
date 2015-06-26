using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Http;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Managers.Base;

namespace ContainerStar.API.Controllers
{
    public enum ActionTypes
    {
        Add,
        Update,
        Delete
    }

    public class ActionSuccessEventArgs<TEntity, TId> : EventArgs
        where TEntity : IHasId<TId>
    {
        public ActionTypes ActionType { get; set; }

        public TEntity Entity { get; set; }
    }

    [DataContract]
    public class GridResult<TModel, TId>
        where TModel : IHasId<TId>, new()
    {
        [DataMember]
        public int total { get; set; }

        [DataMember]
        public IEnumerable<TModel> data { get; set; }
    }

    public abstract class ReadOnlyClientBaseController<TModel, TEntity, TId, TManager> : ApiController
        where TManager : IReadOnlyManagerBase<TEntity, TId>
        where TModel : class, IHasId<TId>, new()
        where TEntity : class, IHasId<TId>
    {
        public ReadOnlyClientBaseController(TManager manager)
        {
            Manager = manager;
        }

        protected abstract void EntityToModel(TEntity entity, TModel model);

        protected TManager Manager { get; private set; }

        public virtual IHttpActionResult Get([FromUri] TId id)
        {
            var entity = Manager.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new TModel();
            model.Id = entity.Id;
            EntityToModel(entity, model);

            return Ok(model);
        }

        public virtual IHttpActionResult Get([FromUri] GridArgs args)
        {
            var entities = GetEntities();

            entities = Sort(entities, args.Sorting);
            entities = Filter(entities, args.Filtering);
            if (entities == null)
            {
                var empty = new GridResult<TModel, TId>
                {
                    total = 0,
                    data = new List<TModel>()
                };

                return Ok(empty);
            }
            var total = entities.Count();

            entities = Page(entities, args.Paging);

            var models = entities.ToList().Select(entity =>
            {
                var model = new TModel();
                EntityToModel(entity, model);
                model.Id = entity.Id;
                return model;
            });

            var result = new GridResult<TModel, TId>
            {
                total = total,
                data = models
            };

            return Ok(result);
        }

        protected virtual IQueryable<TEntity> GetEntities()
        {
            return Manager.GetEntities().AsQueryable();
        }

        private static IQueryable<TEntity> Page(IQueryable<TEntity> entities, Paging paging)
        {
            if (paging.Take > 0)
            {
                entities = entities.Skip(paging.Skip).Take(paging.Take);
            }

            return entities;
        }

        protected virtual IQueryable<TEntity> Sort(IQueryable<TEntity> entities, Sorting sorting)
        {
            var result = entities;

            if (!String.IsNullOrEmpty(sorting.Field))
            {
                var entityType = (typeof (TEntity));
                var property = entityType.GetProperty(sorting.Field,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    result = (typeof (IHasId<TId>).IsAssignableFrom(property.PropertyType))
                        ? entities.OrderBy(sorting.Field + ".ID " + sorting.Direction)
                        : entities.OrderBy(sorting.Field + " " + sorting.Direction);
                }
            }
            else
            {
                result = entities.OrderBy("ID asc");
            }

            return result;
        }

        protected virtual IQueryable<TEntity> Filter(IQueryable<TEntity> entities, Filtering filtering)
        {
            if (filtering.Filters.Any())
            {
                var where = new StringBuilder();

                foreach (var compositeFilter in filtering.Filters)
                {
                    var simpleWhere = new StringBuilder();
                    foreach (var filter in compositeFilter.Filters)
                    {
                        if (simpleWhere.Length > 0)
                        {
                            simpleWhere.Append(" " + ToLinqOperator(compositeFilter.Logic) + " ");
                        }

                        simpleWhere.Append(BuildWhereClause<TEntity>(filter));
                    }

                    if (where.Length > 0)
                    {
                        where.Append(" " + ToLinqOperator(filtering.Logic) + " ");
                    }

                    if (simpleWhere.Length > 0)
                    {
                        where.Append("(" + simpleWhere + ")");
                    }
                }

                entities = entities.Where(where.ToString());
            }

            return entities;
        }

        protected virtual string BuildWhereClause<T>(Filter filter)
        {
            var entityType = (typeof (T));
            PropertyInfo property;

            if (filter.Field.Contains('.'))
            {
                var parts = filter.Field.Split('.');
                var field = parts[0];
                var childField = parts[1];

                property = entityType.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                property = property.PropertyType.GetProperty(childField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }
            else
            {
                property = entityType.GetProperty(filter.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }

            if (typeof (IEnumerable<IHasId<TId>>).IsAssignableFrom(property.PropertyType))
            {
                switch (filter.Operator.ToLower())
                {
                    case "contains":
                        return string.Format("{0}.Any(ID IN ({1}))", filter.Field, filter.Value);
                    default:
                        throw new ArgumentException("This operator is not yet supported for this Grid", filter.Operator);
                }
            }
            if (typeof (IHasId<TId>).IsAssignableFrom(property.PropertyType))
            {
                switch (filter.Operator.ToLower())
                {
                    case "eq":
                    case "neq":
                        return string.Format("{0}.ID {1} {2}", filter.Field, ToLinqOperator(filter.Operator), filter.Value);
                    case "contains":
                        return string.Format("{0}.ID IN ({1})", filter.Field, filter.Value);
                    default:
                        throw new ArgumentException("This operator is not yet supported for this Grid", filter.Operator);
                }
            }
            switch (filter.Operator.ToLower())
            {
                case "eq":
                case "neq":
                case "gte":
                case "gt":
                case "lte":
                case "lt":
                    if (typeof (DateTime).IsAssignableFrom(property.PropertyType) ||
                        typeof (DateTime?).IsAssignableFrom(property.PropertyType))
                    {
                        var date = DateTime.Parse(filter.Value, CultureInfo.InvariantCulture).Date;
                        return string.Format(@"{0} {1} DateTime({2}, {3}, {4})",
                            filter.Field, ToLinqOperator(filter.Operator), date.Year, date.Month, date.Day);
                    }

                    if (typeof (int).IsAssignableFrom(property.PropertyType) || typeof (double).IsAssignableFrom(property.PropertyType))
                    {
                        return string.Format("{0} {1} {2}", filter.Field, ToLinqOperator(filter.Operator), ToFormattedString(filter.Value));
                    }

                    if (typeof(Boolean).IsAssignableFrom(property.PropertyType))
                    {
                        return string.Format("{0} {1} {2}", filter.Field, ToLinqOperator(filter.Operator), filter.Value);
                    }

                    return string.Format("{0} {1} \"{2}\"", filter.Field, ToLinqOperator(filter.Operator), ToFormattedString(filter.Value));
                case "startswith":
                    return string.Format("{0}.StartsWith(\"{1}\")", filter.Field, ToFormattedString(filter.Value));
                case "endswith":
                    return string.Format("{0}.EndsWith(\"{1}\")", filter.Field, ToFormattedString(filter.Value));
                case "contains":
                    return string.Format("{0}.Contains(\"{1}\")", filter.Field, ToFormattedString(filter.Value));
                case "doesnotcontain":
                    return string.Format("!{0}.Contains(\"{1}\") OR {0} == NULL", filter.Field, ToFormattedString(filter.Value));
                default:
                    throw new ArgumentException("This operator is not yet supported for this Grid", filter.Operator);
            }
        }

        protected string ToFormattedString(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return value.Replace("\"", "\"\"");
        }

        private static string ToLinqOperator(string @operator)
        {
            switch (@operator.ToLower())
            {
                case "eq":
                    return " == ";
                case "neq":
                    return " != ";
                case "gte":
                    return " >= ";
                case "gt":
                    return " > ";
                case "lte":
                    return " <= ";
                case "lt":
                    return " < ";
                case "or":
                    return " || ";
                case "and":
                    return " && ";
                default:
                    return null;
            }
        }
    }
}