using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Apps.Backoffice.Backend.Criteria;
using CodelyTv.Backoffice.Courses.Application;
using CodelyTv.Backoffice.Courses.Application.SearchByCriteria;
using CodelyTv.Shared.Domain.Bus.Query;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Backoffice.Backend.Controllers.Courses
{
    [ApiController]
    [Route("/courses")]
    public class CoursesGetController : Controller
    {
        private readonly QueryBus _bus;

        public CoursesGetController(QueryBus bus)
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
