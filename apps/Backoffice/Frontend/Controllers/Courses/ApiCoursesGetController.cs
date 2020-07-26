namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Apps.Backoffice.Frontend.Criteria;
    using CodelyTv.Backoffice.Courses.Application;
    using CodelyTv.Backoffice.Courses.Application.SearchByCriteria;
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

        public async Task<IEnumerable<BackofficeCourseResponse>> Index([FromQuery] FiltersParam param)
        {
            var courses = await _bus.Ask<BackofficeCoursesResponse>(
                new SearchBackofficeCoursesByCriteriaQuery(param.Filters, param.OrderBy, param.Order, param.Limit,
                    param.Offset)
                );

            return courses.Courses;
        }
    }
}