namespace CodelyTv.Mooc.Courses.Domain
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class CourseDuration : StringValueObject
    {
        public CourseDuration(string value) : base(value)
        {
        }
    }
}