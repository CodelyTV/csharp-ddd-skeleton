using CodelyTv.Shared.Domain.ValueObject;

namespace CodelyTv.Shared.Domain.FiltersByCriteria
{
    public class OrderBy : StringValueObject
    {
        public OrderBy(string value) : base(value)
        {
        }
    }
}
