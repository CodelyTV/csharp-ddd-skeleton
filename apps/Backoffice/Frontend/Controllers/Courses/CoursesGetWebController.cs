using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers.Courses
{
    [Route("Courses")]
    public class CoursesGetWebController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            ViewBag.CoursesCounter = 5;
            return View("/Views/Courses/Index.cshtml");
        }
    }
}