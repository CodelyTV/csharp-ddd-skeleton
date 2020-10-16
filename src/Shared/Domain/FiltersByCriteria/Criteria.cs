using System.Linq;

namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    public class Criteria
    {
        public Filters Filters { get; }
        public Order Order { get; }
        public int? Limit { get; }
        public int? Offset { get; }

        public Criteria(Filters filters, Order order, int? limit = null, int? offset = null)
        {
            Filters = filters;
            Order = order;
            Limit = limit;
            Offset = offset;
        }

        public bool HasFilters()
        {
            return Filters != null && Filters.Values.Any();
        }

        public bool HasOrder()
        {
            return Order != null && Order.OrderType != OrderType.NONE && string.IsNullOrEmpty(Order.OrderBy?.Value);
        }
    }
}
