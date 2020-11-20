using System;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class FilterOperatorMother
    {
        public static FilterOperator Random()
        {
            Array values = Enum.GetValues(typeof(FilterOperator));
            Random random = new Random();
            return (FilterOperator)values.GetValue(random.Next(values.Length));
        }
    }
}
