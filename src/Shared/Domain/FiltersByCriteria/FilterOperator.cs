namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    public enum FilterOperator
    {
        EQUAL,
        NOT_EQUAL,
        GT,
        LT,
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
                case "CONTAINS": return FilterOperator.CONTAINS;
                case "NOT_CONTAINS": return FilterOperator.NOT_CONTAINS;
                default: return new FilterOperator();
            }
        }
    }
}