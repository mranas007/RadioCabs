using Microsoft.AspNetCore.Mvc;

namespace RadioCabs.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("Error/403")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
