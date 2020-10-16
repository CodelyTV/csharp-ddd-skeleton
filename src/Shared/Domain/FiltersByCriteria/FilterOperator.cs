namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    public enum FilterOperator
    {
        EQUAL,
        NOT_EQUAL,
        GT,
        GTE,
        LT,
        LTE,
        CONTAINS,
        NOT_CONTAINS
    }

    public static class FilterOperatorExtension
    {
        public static FilterOperator FilterOperatorFromValue(this string value)
        {
            switch (value)
            {
                case "=": return FilterOperator.EQUAL;
                case "!=": return FilterOperator.NOT_EQUAL;
                case ">": return FilterOperator.GT;
                case "<": return FilterOperator.LT;
                case ">=": return FilterOperator.GTE;
                case "<=": return FilterOperator.LTE;
                case "CONTAINS": return FilterOperator.CONTAINS;
                case "NOT_CONTAINS": return FilterOperator.NOT_CONTAINS;
                default: return new FilterOperator();
            }
        }

        public static bool IsPositive(this FilterOperator value)
        {
            return value != FilterOperator.NOT_CONTAINS && value != FilterOperator.NOT_EQUAL;
        }
    }
}
