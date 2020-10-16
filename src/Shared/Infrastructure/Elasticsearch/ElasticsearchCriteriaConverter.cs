using System;
using System.Collections.Generic;
using CodelyTv.Shared.Domain.FiltersByCriteria;
using Nest;
using Filter = CodelyTv.Shared.Domain.FiltersByCriteria.Filter;

namespace CodelyTv.Shared.Infrastructure.Elasticsearch
{
    public class ElasticsearchCriteriaConverter<T> where T : class
    {
        private readonly Dictionary<FilterOperator, Func<Filter, QueryContainer>> _queryTransformers =
            new Dictionary<FilterOperator, Func<Filter, QueryContainer>>
            {
                {FilterOperator.EQUAL, TermQuery},
                {FilterOperator.NOT_EQUAL, TermQuery},
                {FilterOperator.GT, GreaterThanQueryTransformer},
                {FilterOperator.GTE, GreaterThanOrEqualQueryTransformer},
                {FilterOperator.LT, LessThanQueryTransformer},
                {FilterOperator.LTE, LessThanOrEqualQueryTransformer},
                {FilterOperator.CONTAINS, WildcardTransformer},
                {FilterOperator.NOT_CONTAINS, WildcardTransformer}
            };

        public SearchDescriptor<T> Convert(Criteria criteria, string index)
        {
            if (criteria == null) return new SearchDescriptor<T>();

            var searchDescriptor = new SearchDescriptor<T>();

            searchDescriptor.From(criteria.Offset ?? 0);
            searchDescriptor.Size(criteria.Limit ?? 1000);

            if (criteria.HasFilters()) searchDescriptor.Query(q => QueryByCriteria(criteria));
            if (criteria.HasOrder()) searchDescriptor.Sort(s => CreateSortDescriptor(s, criteria));
            searchDescriptor.Index(index);

            return searchDescriptor;
        }

        private SortDescriptor<T> CreateSortDescriptor(SortDescriptor<T> sortDescriptor, Criteria criteria)
        {
            if (!criteria.HasOrder())
                return null;

            var orderByValue = criteria.Order.OrderBy.Value;
            var sortOrder = criteria.Order.OrderType == OrderType.ASC ? SortOrder.Ascending : SortOrder.Descending;

            return sortDescriptor.Field(f => f.Field(orderByValue).Order(sortOrder));
        }

        private QueryContainer QueryByCriteria(Criteria criteria)
        {
            if (!criteria.HasFilters())
                return null;

            QueryContainer query = null;

            foreach (var filter in criteria.Filters.Values)
            {
                var element = _queryTransformers[filter.Operator];
                query &= filter.Operator.IsPositive() ? element(filter) : !element(filter);
            }

            return query;
        }

        private static QueryContainer TermQuery(Filter filter)
        {
            return Query<T>.Term(filter.Field.Value, filter.Value.Value.ToLowerInvariant());
        }

        private static QueryContainer GreaterThanQueryTransformer(Filter filter)
        {
            if (double.TryParse(filter.Value.Value, out var value))
                return Query<T>.Range(r => r.Field(filter.Field.Value).GreaterThan(value));

            return null;
        }

        private static QueryContainer GreaterThanOrEqualQueryTransformer(Filter filter)
        {
            if (double.TryParse(filter.Value.Value, out var value))
                return Query<T>.Range(r => r.Field(filter.Field.Value).GreaterThanOrEquals(value));

            return null;
        }

        private static QueryContainer LessThanQueryTransformer(Filter filter)
        {
            if (double.TryParse(filter.Value.Value, out var value))
                return Query<T>.Range(r => r.Field(filter.Field.Value).LessThan(value));

            return null;
        }

        private static QueryContainer LessThanOrEqualQueryTransformer(Filter filter)
        {
            if (double.TryParse(filter.Value.Value, out var value))
                return Query<T>.Range(r => r.Field(filter.Field.Value).LessThanOrEquals(value));

            return null;
        }

        private static QueryContainer WildcardTransformer(Filter filter)
        {
            return Query<T>.Wildcard(filter.Field.Value, $"*{filter.Value.Value}*");
        }
    }
}
