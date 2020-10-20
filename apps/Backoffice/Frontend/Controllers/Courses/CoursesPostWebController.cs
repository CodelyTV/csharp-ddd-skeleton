using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Application.Create;
using CodelyTv.Shared.Domain.Bus.Command;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    [Route("Courses")]
    public class CoursesPostWebController : Controller
    {
        private const string VIEW = "Views/Courses/Index.cshtml";
        private readonly CommandBus _bus;

        public CoursesPostWebController(CommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Index(CoursesPostWebModel model, int coursesCounter)
        {
            if (ModelState.IsValid)
            {
                await _bus.Dispatch(new CreateCourseCommand(model.Id, model.Name, model.Duration));

                return RedirectToAction("Index", "CoursesGetWeb");
            }

            ViewBag.CoursesCounter = coursesCounter;
            ViewBag.UUID = model.Id;

            return View(VIEW);
        }
    }
}
