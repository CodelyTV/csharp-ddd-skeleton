using System;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class FilterOperatorMother
    {
        public static FilterOperator Random()
        {
            Array values = Enum.GetValues(typeof(FilterOperator));
            if (values.Length == 0)
            {
                throw new InvalidOperationException("The FilterOperator enum does not contain any values.");
            }

            Random random = new Random();
            return (FilterOperator)(values.GetValue(random.Next(values.Length)) ?? throw new InvalidOperationException("Unable to get a random value from the FilterOperator enum."));
        }
    }
}
