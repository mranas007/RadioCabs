using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Data;
using RadioCabs.Models;
using RadioCabs.Models.ViewModel;

namespace RadioCabs.Controllers
{
    [Authorize(Roles = "Driver")]
    public class DriverController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public DriverController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Dashboard()
        {
            ViewData["ActivePage"] = "Dashboard";
            return View();
        }


        // GET: Profile  - page
        public IActionResult Profile()
        {
            ViewData["ActivePage"] = "Profile";
            var userId = _userManager.GetUserId(User);
            var driver = _context.Drivers.FirstOrDefault(i => i.UserId == userId);
            if (driver == null) return NotFound();
            return View(driver);
        }

        // GET: Edit Profile - page
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var driver = _context.Drivers.FirstOrDefault(i => i.Id == id && i.UserId == userId);
            if (driver == null) return NotFound();

            return View(driver);
        }

        //****************************************** POST: Save Changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DriverEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);
            var driver = _context.Drivers.FirstOrDefault(i => i.Id == id && i.UserId == userId);

            // create
            if (driver == null)
            {

            };

            // Update Fields
            driver.DriverName = model.DriverName;
            driver.ContactPerson = model.ContactPerson;
            driver.Address = model.Address;
            driver.City = model.City;
            driver.Experience = model.Experience;
            driver.Description = model.Description;

            _context.SaveChanges();

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile", new { id = driver.Id });
        }

        // GET: Services - page
        public IActionResult Services()
        {
            ViewData["ActivePage"] = "Services";
            var userId = _userManager.GetUserId(User);
            var payment = _context.Payments.FirstOrDefault(u => u.UserId == userId && (u.PaymentType == "Monthly" || u.PaymentType == "Quarterly") && u.IsPaid && u.ExpiryDate > DateTime.Now);

            if (payment == null)
            {
                TempData["Error"] = "You need a valid Basic or Premium subscription to access this service.";
                return RedirectToAction("Subscription");
            }

            var companies = _context.Advertisements.Include(c => c.Company).ToList();
            return View(companies);
        }

        // GET: Services Details Page
        public IActionResult ServicesDetails(int id)
        {
            var company = _context.Advertisements
                                  .Include(c => c.Company)
                                  .ThenInclude(c => c.User)
                                  .FirstOrDefault(c => c.Id == id);

            if (company == null) return NotFound();

            return View(company);
        }

        // GET: Subscription Page
        public async Task<IActionResult> Subscription(int id)
        {
            ViewData["ActivePage"] = "Subscription";
            var userId = _userManager.GetUserId(User);
            var payment = await _context.Payments
                .Where(u => u.UserId == userId
                            && (u.PaymentType == "Monthly" || u.PaymentType == "Quarterly")
                            && u.IsPaid
                            && u.ExpiryDate > DateTime.Now)
                .FirstOrDefaultAsync(); // Using async for better performance

            var model = new SubscriptionVM
            {
                IsSubscribed = payment != null,
                CurrentPlan = payment?.PaymentType ?? "none",
                LastPaymentDate = payment?.ExpiryDate.ToString("yyyy-MM-dd") ?? "N/A"
            };

            return View(model);
        }


    }
}
