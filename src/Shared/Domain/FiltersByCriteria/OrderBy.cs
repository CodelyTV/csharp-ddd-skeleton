namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class OrderBy : StringValueObject
    {
        public OrderBy(string value) : base(value)
        {
        }
    }
}