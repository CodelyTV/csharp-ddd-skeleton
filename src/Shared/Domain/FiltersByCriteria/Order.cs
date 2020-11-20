using System;

namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    public class Order
    {
        public OrderBy OrderBy { get; }
        public OrderType OrderType { get; }

        public Order(OrderBy orderBy, OrderType orderType)
        {
            OrderBy = orderBy;
            OrderType = orderType;
        }

        public static Order FromValues(string orderBy, string orderType)
        {
            return new Order(
                new OrderBy(orderBy),
                !string.IsNullOrEmpty(orderType)
                    ? Enum.Parse<OrderType>(orderType.ToUpperInvariant())
                    : OrderType.NONE);
        }

        public static Order None()
        {
            return new Order(new OrderBy(string.Empty), OrderType.NONE);
        }
    }
}
