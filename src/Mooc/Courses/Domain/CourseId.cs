namespace Mooc.Courses.Domain
{
    using Shared.Domain.ValueObject;

    public class CourseId : Uuid
    {
        public CourseId(string value) : base(value)
        {
        }
    }
}