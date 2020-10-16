using CodelyTv.Mooc.CoursesCounters.Application.Find;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Mooc.CoursesCounters.Application.Find
{
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
