namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class FilterValue : StringValueObject
    {
        public FilterValue(string value) : base(value)
        {
        }
    }
}