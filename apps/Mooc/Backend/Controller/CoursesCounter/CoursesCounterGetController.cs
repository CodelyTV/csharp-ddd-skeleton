namespace CodelyTv.Apps.Mooc.Backend.Controller.CoursesCounter
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Domain.Bus.Query;

    [Route("courses-counter")]
    public class CoursesCounterGetController : Controller
    {
        private readonly IQueryBus _bus;

        public CoursesCounterGetController(IQueryBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Index()
        {
            CoursesCounterResponse response = await _bus.Ask<CoursesCounterResponse>(new FindCoursesCounterQuery());

            return Ok(new Dictionary<string, int>()
            {
                {"total", response.Total}
            });
        }
    }
}