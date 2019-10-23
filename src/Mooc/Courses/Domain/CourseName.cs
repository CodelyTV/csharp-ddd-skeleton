namespace CodelyTv.Mooc.Courses.Domain
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class CourseName : StringValueObject
    {
        public CourseName(string value) : base(value)
        {
        }
    }
}