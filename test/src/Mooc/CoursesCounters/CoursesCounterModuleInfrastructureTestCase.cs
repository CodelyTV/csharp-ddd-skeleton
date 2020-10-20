using CodelyTv.Mooc.CoursesCounters.Domain;

namespace CodelyTv.Test.Mooc.CoursesCounters
{
    public class CoursesCounterModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected CoursesCounterRepository Repository => GetService<CoursesCounterRepository>();
    }
}
