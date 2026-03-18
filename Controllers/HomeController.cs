using Microsoft.AspNetCore.Mvc;

namespace MyTripLog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Trips");
        }
    }
}