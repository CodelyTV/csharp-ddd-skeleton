using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Mooc.CoursesCounters.Domain
{
    public class CoursesCounterTotalMother
    {
        public static CoursesCounterTotal Create(int value)
        {
            return new CoursesCounterTotal(value);
        }

        public static CoursesCounterTotal Random()
        {
            return Create(IntegerMother.Random());
        }

        public static CoursesCounterTotal One()
        {
            return Create(1);
        }
    }
}
