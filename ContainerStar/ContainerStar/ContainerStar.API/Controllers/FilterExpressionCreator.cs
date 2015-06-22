using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using ContainerStar.Contracts;

namespace ContainerStar.API.Controllers
{
    public interface IFilterExpressionCreator
    {
        IQueryable<T> Filter<T>(IQueryable<T> entities, Filtering filtering);
        IQueryable<T> Filter<T>(IQueryable<T> entities, Filtering filtering, Func<Filter, string> buildWhereClauseHandler);
    }

    public class FilterExpressionCreator : IFilterExpressionCreator
    {
        public IQueryable<T> Filter<T>(IQueryable<T> entities, Filtering filtering)
        {
            return Filter<T>(entities, filtering, BuildWhereClause<T>);
        }

        public IQueryable<T> Filter<T>(IQueryable<T> entities, Filtering filtering, Func<Filter, string> buildWhereClauseHandler)
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
                            simpleWhere.Append(" " + ToLinqOperator(compositeFilter.Logic) + " ");

                        simpleWhere.Append(buildWhereClauseHandler(filter));
                    }

                    if (where.Length > 0)
                        where.Append(" " + ToLinqOperator(filtering.Logic) + " ");

                    if (simpleWhere.Length > 0)
                        where.Append("(" + simpleWhere.ToString() + ")");
                }

                entities = entities.Where(where.ToString());
            }

            return entities;
        }

        public static string BuildWhereClause<T>(Filter filter)
        {
            var entityType = (typeof(T));
            PropertyInfo property;

            if (filter.Field.Contains('.'))
            {
                var parts = filter.Field.Split('.');
                property = entityType.GetProperty(parts[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                for (var i = 0; i < parts.Length - 1; i++)
                    property = property.PropertyType.GetProperty(parts[i + 1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }
            else
                property = entityType.GetProperty(filter.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (typeof(IEnumerable<IHasId<int>>).IsAssignableFrom(property.PropertyType))
            {
                switch (filter.Operator.ToLower())
                {
                    case "contains":
                        return string.Format("{0}.Any(ID IN ({1}))", filter.Field, filter.Value);
                    default:
                        throw new ArgumentException("This operator is not yet supported for this Grid", filter.Operator);
                }
            }
            if (typeof(IHasId<int>).IsAssignableFrom(property.PropertyType))
            {
                switch (filter.Operator.ToLower())
                {
                    case "eq":
                    case "neq":
                        if (string.IsNullOrWhiteSpace(filter.Value))
                            return string.Format("{0} {1}", filter.Field, ToCompareToNullOperator(filter.Operator));

                        return string.Format("{0}.ID {1} {2}", filter.Field, ToLinqOperator(filter.Operator), filter.Value);
                    case "contains":
                        return string.Format("{0}.ID IN ({1})", filter.Field, filter.Value);
                    default:
                        throw new ArgumentException("This operator is not yet supported for this Grid", filter.Operator);
                }
            }
            else
            {
                switch (filter.Operator.ToLower())
                {
                    case "eq":
                    case "neq":
                    case "gte":
                    case "gt":
                    case "lte":
                    case "lt":
                        if (property.PropertyType.IsClass && string.IsNullOrWhiteSpace(filter.Value))
                            return string.Format("{0} {1}", filter.Field, ToCompareToNullOperator(filter.Operator));

                        if (typeof(DateTime).IsAssignableFrom(property.PropertyType) ||
                            typeof(DateTime?).IsAssignableFrom(property.PropertyType))
                        {
                            var date = DateTime.Parse(filter.Value, CultureInfo.InvariantCulture).Date;
                            return string.Format(@"EntityFunctions.TruncateTime({0}) {1} DateTime({2}, {3}, {4})",
                                filter.Field, ToLinqOperator(filter.Operator), date.Year, date.Month, date.Day);
                        }

                        if (typeof(int).IsAssignableFrom(property.PropertyType) || typeof(double).IsAssignableFrom(property.PropertyType) ||
                            typeof(int?).IsAssignableFrom(property.PropertyType) || typeof(double?).IsAssignableFrom(property.PropertyType))
                            return string.Format("{0} {1} {2}", filter.Field, ToLinqOperator(filter.Operator), ToFormattedString(filter.Value));

                        return string.Format("{0} {1} \"{2}\"", filter.Field, ToLinqOperator(filter.Operator), ToFormattedString(filter.Value));
                    case "startswith":
                        return string.Format("{0}.StartsWith(\"{1}\")", filter.Field, ToFormattedString(filter.Value));
                    case "endswith":
                        return string.Format("{0}.EndsWith(\"{1}\")", filter.Field, ToFormattedString(filter.Value));
                    case "contains":
                        if (typeof(int).IsAssignableFrom(property.PropertyType) || typeof(int?).IsAssignableFrom(property.PropertyType))
                            return string.Format("{0} IN ({1})", filter.Field, filter.Value);

                        return string.Format("{0}.Contains(\"{1}\")", filter.Field, ToFormattedString(filter.Value));
                    case "doesnotcontain":
                        return string.Format("!{0}.Contains(\"{1}\") OR {0} == NULL", filter.Field, ToFormattedString(filter.Value));
                    default:
                        throw new ArgumentException("This operator is not yet supported for this Grid", filter.Operator);
                }
            }
        }

        private static string ToFormattedString(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return value;

            return value.Replace("\"", "\"\"");
        }

        public static string ToLinqOperator(string @operator)
        {
            switch (@operator.ToLower())
            {
                case "eq": return " == ";
                case "neq": return " != ";
                case "gte": return " >= ";
                case "gt": return " > ";
                case "lte": return " <= ";
                case "lt": return " < ";
                case "or": return " || ";
                case "and": return " && ";
                default: return null;
            }
        }

        public static string ToCompareToNullOperator(string @operator)
        {
            switch (@operator.ToLower())
            {
                case "eq": return " == NULL";
                case "neq": return " != NULL";
                default: return null;
            }
        }
    }
}
