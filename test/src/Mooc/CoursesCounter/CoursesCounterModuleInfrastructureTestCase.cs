namespace CodelyTv.Tests.Mooc.CoursesCounter
{
    using CodelyTv.Mooc.Courses.Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.XUnit;

    public class CoursesCounterModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected ICourseRepository Repository()
        {
            return this.Host.Services.GetService<ICourseRepository>();
        }
    }
}