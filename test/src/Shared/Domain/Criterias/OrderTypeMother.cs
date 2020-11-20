using System;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class OrderTypeMother
    {
        public static OrderType Random()
        {
            Array values = Enum.GetValues(typeof(OrderType));
            Random random = new Random();
            return (OrderType)values.GetValue(random.Next(values.Length));
        }
    }
}
