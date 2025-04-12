using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RadioCabs.Data;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // ---------------------- Index
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (User.IsInRole("Company"))
                    {
                        return RedirectToAction("Dashboard", "Company");
                    }
                    else if (User.IsInRole("Driver"))
                    {
                        return RedirectToAction("Dashboard", "Driver");
                    }
                }
            }
            return View();
        }

        // ---------------------- AboutUs
        public IActionResult AboutUs()
        {
            return View();
        }

        // ---------------------- Feedback
        public IActionResult Feedback()
        {
            return View(new Feedback()); // new empty model is passed
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Feedback(Feedback model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Feedback submission failed. Please check the errors.";
                return View(model);
            }

            _context.Feedbacks.Add(model);
            _context.SaveChanges(); // Save to the database

            TempData["Success"] = "We have successfully received your feedback!";

            return RedirectToAction(nameof(Index)); // Redirect to clear the form after submission
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
