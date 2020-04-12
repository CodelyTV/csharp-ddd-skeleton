namespace CodelyTv.Test.Mooc.CoursesCounter.Domain
{
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Test.Shared.Domain;

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