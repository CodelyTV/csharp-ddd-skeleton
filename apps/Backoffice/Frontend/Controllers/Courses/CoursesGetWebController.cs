using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers.Courses
{
    [Route("Course")]
    public class CourseGetWebController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            return View("/Views/Courses/Index.cshtml");
        }
    }
}