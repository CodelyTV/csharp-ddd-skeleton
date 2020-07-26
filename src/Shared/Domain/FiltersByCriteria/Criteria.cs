namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    public class Criteria
    { 
        public Filters Filters { get; }
        public Order Order { get; }
        public int? Limit { get; }
        public int? Offset { get; }
        
        public Criteria(Filters filters, Order order,  int? limit = null, int? offset = null)
        {
            Filters = filters;
            Order = order;
            Limit = limit;
            Offset = offset;
        }
    }
}