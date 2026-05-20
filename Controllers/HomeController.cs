using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Red.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
