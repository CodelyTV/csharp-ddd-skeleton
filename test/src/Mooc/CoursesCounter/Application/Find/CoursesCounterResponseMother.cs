namespace CodelyTv.Tests.Mooc.CoursesCounter.Application.Find
{
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using Test.Shared.Domain;

    public static class CoursesCounterResponseMother
    {
        public static CoursesCounterResponse Create(int value)
        {
            return new CoursesCounterResponse(value);
        }

        public static CoursesCounterResponse Random()
        {
            return Create(IntegerMother.Random());
        }
    }
}