using CodelyTv.Shared.Domain.ValueObject;

namespace CodelyTv.Mooc.CoursesCounters.Domain
{
    public class CoursesCounterId : Uuid
    {
        public CoursesCounterId(string value) : base(value)
        {
        }
    }
}
