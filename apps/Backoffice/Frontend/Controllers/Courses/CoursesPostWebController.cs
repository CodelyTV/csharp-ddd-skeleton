namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mooc.Courses.Application.Create;
    using Shared.Domain.Bus.Command;

    public class CoursesPostWebController : Controller
    {
        private readonly ICommandBus _bus;

        public CoursesPostWebController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Index(CoursesPostWebModel model)
        {
            if (ModelState.IsValid)
            {
                await _bus.Dispatch(new CreateCourseCommand(model.Id, model.Name, model.Duration));

                return RedirectToAction("Index", "CoursesGetWeb");
            }

            return View("Views/Courses/Courses.cshtml");
        }
    }
}