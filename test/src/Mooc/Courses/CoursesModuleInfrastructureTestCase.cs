namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Domain;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared.Infrastructure.XUnit;

    public abstract class CoursesModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected ICourseRepository Repository(TestServer server)
        {
            return server.Host.Services.GetService<ICourseRepository>();
        }
    }
}