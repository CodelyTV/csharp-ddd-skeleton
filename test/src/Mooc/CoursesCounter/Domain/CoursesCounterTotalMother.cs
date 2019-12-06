namespace CodelyTv.Tests.Mooc.CoursesCounter.Domain
{
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Test.Shared.Domain;

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