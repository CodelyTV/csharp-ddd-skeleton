namespace CodelyTv.Test.Mooc.CoursesCounter
{
    using CodelyTv.Mooc.CoursesCounter.Domain;

    public class CoursesCounterModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected ICoursesCounterRepository Repository => GetService<ICoursesCounterRepository>();
    }
}