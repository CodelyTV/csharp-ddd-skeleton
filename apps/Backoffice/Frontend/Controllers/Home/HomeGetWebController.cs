using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Home
{
    public class HomeGetWebController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            return View("/Views/Home/Index.cshtml");
        }
    }
}
