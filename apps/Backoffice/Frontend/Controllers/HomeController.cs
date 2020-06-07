namespace Frontend.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Welcome";
            ViewBag.Description = "CodelyTV - Backoffice";
            return View();
        }
    }
}