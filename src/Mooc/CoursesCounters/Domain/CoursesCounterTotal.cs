using CodelyTv.Shared.Domain.ValueObject;

namespace CodelyTv.Mooc.CoursesCounters.Domain
{
    public class CoursesCounterTotal : IntValueObject
    {
        public CoursesCounterTotal(int value) : base(value)
        {
        }

        public static CoursesCounterTotal Initialize()
        {
            return new CoursesCounterTotal(0);
        }

        public CoursesCounterTotal Increment()
        {
            return new CoursesCounterTotal(Value + 1);
        }
    }
}
