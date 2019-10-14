namespace Mooc.Courses.Domain
{
    using Shared.Domain.ValueObject;

    public class CourseDuration : StringValueObject
    {
        public CourseDuration(string value) : base(value)
        {
        }
    }
}