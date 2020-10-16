using CodelyTv.Shared.Domain.ValueObject;

namespace CodelyTv.Mooc.Courses.Domain
{
    public class CourseId : Uuid
    {
        public CourseId(string value) : base(value)
        {
        }
    }
}
