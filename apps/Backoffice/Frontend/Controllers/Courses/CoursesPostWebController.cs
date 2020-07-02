namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mooc.Courses.Application.Create;
    using Shared.Domain.Bus.Command;
    
    [Route("Courses")]
    public class CoursesPostWebController : Controller
    {
        private readonly ICommandBus _bus;
        private const string VIEW = "Views/Courses/Index.cshtml";
        public CoursesPostWebController(ICommandBus bus)
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