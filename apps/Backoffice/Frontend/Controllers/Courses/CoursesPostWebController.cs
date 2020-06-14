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
        public async Task<IActionResult> Index(string id, string name, string duration)
        {
            await _bus.Dispatch(new CreateCourseCommand(id, name, duration));

            return RedirectToAction("Index", "CoursesGetWeb");
        }
    }
}