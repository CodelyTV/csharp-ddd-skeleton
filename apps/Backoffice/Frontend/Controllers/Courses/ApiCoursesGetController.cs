namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Backoffice.Courses.Application;
    using CodelyTv.Backoffice.Courses.Application.SearchAll;
    using CodelyTv.Shared.Domain.Bus.Query;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/courses")]
    public class ApiCoursesGetController
    {
        private readonly IQueryBus _bus;

        public ApiCoursesGetController(IQueryBus bus)
        {
            _bus = bus;
        }

        public async Task<IEnumerable<BackofficeCourseResponse>> Index()
        {
            var courses = await _bus.Ask<BackofficeCoursesResponse>(new SearchAllBackofficeCoursesQuery());

            return courses.Courses;
        }
    }
}