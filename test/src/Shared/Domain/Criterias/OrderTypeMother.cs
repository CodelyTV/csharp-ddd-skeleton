using System;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class OrderTypeMother
    {
        public static OrderType Random()
        {
            Array values = Enum.GetValues(typeof(OrderType));
            if (values.Length == 0)
            {
                throw new InvalidOperationException("The OrderType enum does not contain any values.");
            }

            Random random = new Random();
            return (OrderType)(values.GetValue(random.Next(values.Length)) ?? throw new InvalidOperationException("Unable to get a random value from the OrderType enum."));
        }
    }
}
