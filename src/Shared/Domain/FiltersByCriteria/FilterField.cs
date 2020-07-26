namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class FilterField : StringValueObject
    {
        public FilterField(string value) : base(value)
        {
        }
    }
}