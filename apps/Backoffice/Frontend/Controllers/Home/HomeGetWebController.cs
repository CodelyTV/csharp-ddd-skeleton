namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Home
{
    using Microsoft.AspNetCore.Mvc;

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