namespace Mooc.Courses.Domain
{
    using Shared.Domain.ValueObject;

    public class CourseName : StringValueObject
    {
        public CourseName(string value) : base(value)
        {
        }
    }
}