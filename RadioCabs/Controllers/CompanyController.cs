using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Data;
using RadioCabs.Models;
using RadioCabs.Models.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RadioCabs.Controllers
{
    [Authorize(Roles = "Company")]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CompanyController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dashboard  - page
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var company = await _context.Companies.FirstOrDefaultAsync(u => u.UserId == userId);

            if (company == null)
            {
                return RedirectToAction("Register", "Account");
            }

            // Count advertisements by this company
            int advertisementCount = await _context.Advertisements.CountAsync(a => a.CompanyId == company.Id);

            // Get latest membership payment
            var latestPayment = await _context.Payments
                                        .Where(p => p.UserId == userId && p.IsPaid)
                                        .OrderByDescending(p => p.ExpiryDate)
                                        .FirstOrDefaultAsync();

            var model = new DashboardVM
            {
                AdvertisementCount = advertisementCount,
                MembershipPlan = latestPayment?.PaymentType ?? "No active plan",
                NextPaymentDate = latestPayment?.ExpiryDate
            };

            return View(model);
        }

        // GET: Profile  - page
        public IActionResult Profile()
        {
            var userId = _userManager.GetUserId(User);
            var company = _context.Companies.FirstOrDefault(i => i.UserId == userId);
            if (company == null) return NotFound();
            return View(company);
        }

        public IActionResult DriverList()
        {
            var drivers = _context.Drivers
                .Include(d => d.User)
                .Where(d => d.User.Payments.Any(p => p.IsPaid))
                .ToList();

            return View(drivers);
        }

        public IActionResult DriverDetails(int Id)
        {
            var drivers = _context.Drivers.Include(u => u.User).FirstOrDefault(i => i.Id == Id);
            if (drivers == null)
            {
                return BadRequest();
            }
            return View(drivers);
        }

        // GET: Edit Profile - page
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var company = _context.Companies.FirstOrDefault(i => i.Id == id && i.UserId == userId);

            if (company == null)
                return NotFound();
            var model = new CompanyEditVM()
            {
                CompanyName = company.CompanyName,
                Designation = company.Designation,
                Address = company.Address,
            };
            // No need to map to CompanyEditVM, pass the Company entity directly to the view
            return View(model); // Pass the Company model to the view
        }


        //************************ POST: Save Changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CompanyEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model); // Return the model to the view if invalid

            var userId = _userManager.GetUserId(User);
            var company = _context.Companies.FirstOrDefault(i => i.Id == id && i.UserId == userId);

            if (company == null)
                return BadRequest();

            // Update the Company entity using the ViewModel
            company.CompanyName = model.CompanyName;
            company.Designation = model.Designation;
            company.Address = model.Address;

            _context.SaveChanges();

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile", new { id = company.Id });
        }

        // --------------------------------------------- Advertisement section ---------------------------------------------- //
        // GET: ListAd - page
        public IActionResult ListAd()
        {
            var userID = _userManager.GetUserId(User);
            var ads = _context.Advertisements.Include(a => a.Company).Where(a => a.Company.UserId == userID).ToList();
            return View(ads);
        }

        // GET: CreateAd - page
        public IActionResult CreateAd()
        {
            var userId = _userManager.GetUserId(User);
            var company = _context.Companies.FirstOrDefault(c => c.UserId == userId);

            if (company == null)
            {
                return NotFound();
            }

            if (company.Membership == "Free")
            {
                TempData["Error"] = "Free membership cannot advertise services.";
                return RedirectToAction("Subscription");
            }

            return View();
        }

        //************************  POST: Save form 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAd(Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewBag.ValidationErrors = errors;
                return View(advertisement);
            }

            var userId = _userManager.GetUserId(User);
            var company = _context.Companies.FirstOrDefault(c => c.UserId == userId);

            if (company == null)
            {
                return NotFound();
            }

            if (company.Membership == "Free")
            {
                TempData["Error"] = "Free membership cannot advertise services.";
                return RedirectToAction("Subscription");
            }

            int maxAdvertisements = company.Membership == "Basic" ? 10 : 20;
            int currentAdvertisements = _context.Advertisements.Count(a => a.CompanyId == company.Id);

            if (currentAdvertisements >= maxAdvertisements)
            {
                TempData["Error"] = $"You have reached the maximum limit of {maxAdvertisements} advertisements for your membership level.";
                return RedirectToAction("AdvertiseService");
            }

            advertisement.CompanyId = company.Id;
            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();

            TempData["Success"] = "Advertisement created successfully!";
            return RedirectToAction("ListAd");
        }


        // GET: Edit Advertisement - page
        public IActionResult EditAd(int id)
        {
            var ad = _context.Advertisements.Find(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        //************************ POST: Edit Advertisement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAd(int id, Advertisement ad)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewBag.ValidationErrors = errors;
                return View();
            }

            if (id != ad.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var company = _context.Companies.FirstOrDefault(c => c.UserId == userId);
            if (company == null || company.Id != ad.CompanyId)
            {
                ViewBag.ValidationErrors = "Invalid CompanyId.";
                return View(ad);
            }

            _context.Advertisements.Update(ad);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListAd));
        }

        //************************ POST: Delete Advertisement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAd(int id)
        {
            var ad = _context.Advertisements.Find(id);
            if (ad == null)
            {
                return NotFound();
            }

            _context.Advertisements.Remove(ad);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListAd));
        }

        // --------------------------------------------- Subscription section ---------------------------------------------- //

        public async Task<IActionResult> Subscription()
        {
            var userId = _userManager.GetUserId(User);
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.UserId == userId);
            var payment = await _context.Payments.Where(u => u.UserId == userId
                            && (u.PaymentType == "Monthly" || u.PaymentType == "Quarterly")
                            && u.IsPaid
                            && u.ExpiryDate > DateTime.Now)
                            .FirstOrDefaultAsync();
            if (company == null)
            {
                return RedirectToAction("Register", "Account", new { usertype = "Company" });
            }

            bool isSubscribed = company.Membership != "Free"; // Assume "Free" means no subscription

            var model = new SubscriptionVM
            {
                IsSubscribed = isSubscribed,
                CurrentPlan = company.Membership,
                LastPaymentDate = payment?.ExpiryDate.ToString("yyyy-MM-dd") ?? "N/A"
            };

            return View(model);
        }

    }
}
