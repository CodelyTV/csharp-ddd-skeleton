using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Criteria
{
    public static class LinqBuilderByCriteria
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> collection, Domain.FiltersByCriteria.Criteria criteria)
        {
            if (criteria?.Filters == null || !criteria.Filters.Values.Any()) return collection;

            var queries = new List<string>();
            string query;

            foreach (var filter in criteria.Filters.Values)
            {
                query = GetQueryByFilter(filter);

                if (!string.IsNullOrEmpty(query)) queries.Add(query);
            }

            return collection.Where(string.Join(" && ", queries));
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection,
            Domain.FiltersByCriteria.Criteria criteria)
        {
            if (criteria == null || criteria.Order?.OrderBy?.Value == null ||
                criteria.Order.OrderType == OrderType.NONE)
                return collection;

            switch (criteria.Order.OrderType)
            {
                case OrderType.ASC:
                    return collection.OrderBy(criteria.Order.OrderBy.Value);
                case OrderType.DESC:
                    return collection.OrderBy($"{criteria.Order.OrderBy.Value} DESC");
            }

            return collection;
        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection,
            Domain.FiltersByCriteria.Criteria criteria)
        {
            if (criteria?.Limit == null || criteria.Limit.Value == 0) return collection;

            return collection.Take(criteria.Limit.GetValueOrDefault());
        }

        public static IQueryable<T> Offset<T>(this IQueryable<T> collection,
            Domain.FiltersByCriteria.Criteria criteria)
        {
            if (criteria?.Offset == null) return collection;

            return collection.Skip(criteria.Offset.GetValueOrDefault());
        }


        private static string GetQueryByFilter(Filter filter)
        {
            switch (filter.Operator)
            {
                case FilterOperator.EQUAL:
                    return $"{filter.Field.Value} == \"{filter.Value.Value}\"";
                case FilterOperator.NOT_EQUAL:
                    return $"{filter.Field.Value} != \"{filter.Value.Value}\"";
                case FilterOperator.GT:
                    return $"{filter.Field.Value} > {filter.Value.Value}";
                case FilterOperator.LT:
                    return $"{filter.Field.Value} < {filter.Value.Value}";
                case FilterOperator.CONTAINS:
                    return $"{filter.Field.Value}.Contains(\"{filter.Value.Value}\")";
                case FilterOperator.NOT_CONTAINS:
                    return $"!{filter.Field.Value}.Contains(\"{filter.Value.Value}\")";
            }

            return string.Empty;
        }
    }
}
