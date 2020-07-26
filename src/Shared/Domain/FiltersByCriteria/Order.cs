namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    using System;

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
                !string.IsNullOrEmpty(orderType) ? Enum.Parse<OrderType>(orderType.ToUpper()) : OrderType.NONE);
        }
    }
}