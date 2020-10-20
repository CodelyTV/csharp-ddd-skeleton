using System;
using System.Threading.Tasks;
using CodelyTv.Mooc.CoursesCounters.Application.Find;
using CodelyTv.Shared.Domain.Bus.Query;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    [Route("Courses")]
    public class CoursesGetWebController : Controller
    {
        private const string VIEW = "Views/Courses/Index.cshtml";
        private readonly QueryBus _bus;

        public CoursesGetWebController(QueryBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var counterResponse =
                await _bus.Ask<CoursesCounterResponse>(new FindCoursesCounterQuery());

            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            ViewBag.CoursesCounter = counterResponse.Total;
            ViewBag.UUID = Guid.NewGuid().ToString();

            return View(VIEW);
        }
    }
}
