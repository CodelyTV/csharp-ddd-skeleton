using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Mooc.CoursesCounters.Domain
{
    public class CoursesCounterIdMother
    {
        public static CoursesCounterId Create(string value)
        {
            return new CoursesCounterId(value);
        }

        public static CoursesCounterId Random()
        {
            return Create(UuidMother.Random());
        }
    }
}
