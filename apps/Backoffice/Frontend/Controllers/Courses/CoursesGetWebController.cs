namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    using System;
    using System.Threading.Tasks;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Shared.Domain.Bus.Query;
    using Microsoft.AspNetCore.Mvc;

    [Route("Courses")]
    public class CoursesGetWebController : Controller
    {
        private readonly IQueryBus _bus;
        private const string VIEW = "Views/Courses/Index.cshtml";

        public CoursesGetWebController(IQueryBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CoursesCounterResponse counterResponse =
                await _bus.Ask<CoursesCounterResponse>(new FindCoursesCounterQuery());

            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            ViewBag.CoursesCounter = counterResponse.Total;
            ViewBag.UUID = Guid.NewGuid().ToString();

            return View(VIEW);
        }
    }
}