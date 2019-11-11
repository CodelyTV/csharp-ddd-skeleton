namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.XUnit;

    public abstract class CoursesModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected ICourseRepository Repository()
        {
            return this.TestServer.Host.Services.GetService<ICourseRepository>();
        }
    }
}