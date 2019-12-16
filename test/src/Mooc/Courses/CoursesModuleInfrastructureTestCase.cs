namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.XUnit;

    public abstract class CoursesModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected ICourseRepository Repository
        {
            get { return this.Host.Services.GetService<ICourseRepository>(); }
        }
    }
}