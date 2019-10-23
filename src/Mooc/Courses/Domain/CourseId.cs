namespace CodelyTv.Mooc.Courses.Domain
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class CourseId : Uuid
    {
        public CourseId(string value) : base(value)
        {
        }
    }
}