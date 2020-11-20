using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class OrderMother
    {
        public static Order Create(OrderBy orderBy, OrderType orderType)
        {
            return new Order(orderBy, orderType);
        }

        public static Order CreateDesc(OrderBy orderBy)
        {
            return new Order(orderBy, OrderType.DESC);
        }

        public static Order None()
        {
            return Order.None();
        }

        public static Order Random()
        {
            return Create(OrderByMother.Random(), OrderTypeMother.Random());
        }
    }
}
