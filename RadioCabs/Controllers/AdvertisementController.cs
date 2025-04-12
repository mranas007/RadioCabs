using Microsoft.AspNetCore.Mvc;

namespace RadioCabs.Controllers
{
    public class AdvertisementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
