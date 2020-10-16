using CodelyTv.Shared.Domain.ValueObject;

namespace CodelyTv.Mooc.Courses.Domain
{
    public class CourseName : StringValueObject
    {
        public CourseName(string value) : base(value)
        {
        }
    }
}
