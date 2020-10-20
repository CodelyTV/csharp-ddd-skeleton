using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Mooc.CoursesCounters.Application.Find;
using CodelyTv.Shared.Domain.Bus.Query;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Mooc.Backend.Controller.CoursesCounter
{
    [Route("courses-counter")]
    public class CoursesCounterGetController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly QueryBus _bus;

        public CoursesCounterGetController(QueryBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Index()
        {
            var response = await _bus.Ask<CoursesCounterResponse>(new FindCoursesCounterQuery());

            return Ok(new Dictionary<string, int>
            {
                {"total", response.Total}
            });
        }
    }
}
