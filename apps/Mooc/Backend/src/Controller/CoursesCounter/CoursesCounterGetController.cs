namespace CodelyTv.Apps.Mooc.Backend.Controller.CoursesCounter
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using Microsoft.AspNetCore.Mvc;

    [Route("courses-counter")]
    public class CoursesCounterGetController : Controller
    {
        private readonly CoursesCounterFinder _finder;

        public CoursesCounterGetController(CoursesCounterFinder finder)
        {
            this._finder = finder;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Index()
        {
            CoursesCounterResponse response = _finder.Find();

            return Ok(new Dictionary<string, int>()
            {
                {"total", response.Total}
            });
        }
    }
}