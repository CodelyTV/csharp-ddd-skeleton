namespace Frontend.Controllers.Courses
{
    using System.Threading.Tasks;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Shared.Domain.Bus.Query;
    using Microsoft.AspNetCore.Mvc;

    [Route("Courses")]
    public class CoursesGetWebController : Controller
    {
        private readonly IQueryBus _bus;

        public CoursesGetWebController(IQueryBus bus)
        {
            _bus = bus;
        }

        public async Task<IActionResult> Index()
        {
            CoursesCounterResponse counterResponse =
                await _bus.Ask<CoursesCounterResponse>(new FindCoursesCounterQuery());

            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            ViewBag.CoursesCounter = counterResponse.Total;
            return View("/Views/Courses/Index.cshtml");
        }
    }
}