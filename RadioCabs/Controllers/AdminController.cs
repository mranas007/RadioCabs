using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Data;
using RadioCabs.Models;
using RadioCabs.Models.ViewModel;

namespace RadioCabs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ----------------------------------------------------------------- Dashboard section.
        // GET: Dashboard - page
        public IActionResult Dashboard()
        {
            var viewModel = new AdminDashboardVM
            {
                TotalUsers = _context.Users.Count(),
                TotalDrivers = _context.Drivers.Count(),
                TotalCompanies = _context.Companies.Count(),
                PaidPayments = _context.Payments.Count(p => p.IsPaid),
                TotalFeedbacks = _context.Feedbacks.Count()
            };

            return View(viewModel);
        }

        // ----------------------------------------------------------------- User section.
        public async Task<IActionResult> UserList()
        {
            // Get admin user IDs first
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminUserIds = adminUsers.Select(u => u.Id).ToList();

            // Get all non-admin users
            var users = await _context.Users
                .Where(u => !adminUserIds.Contains(u.Id))
                .ToListAsync();

            return View(users);
        }
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            if (user.UserType == "Driver")
            {
                var driver = await _context.Drivers.Include(u => u.User).Where(u => u.UserId == user.Id).FirstOrDefaultAsync();
                if (driver == null)
                {
                    TempData["Error"] = "Sorry the User does not Exist.";
                    return RedirectToAction(nameof(UserList));
                }
                return RedirectToAction(nameof(DriverDetails), new { id = driver.Id });
            }
            else if (user.UserType == "Company")
            {
                var company = await _context.Companies.Include(u => u.User).Where(u => u.UserId == user.Id).FirstOrDefaultAsync();
                if (company == null)
                {
                    TempData["Error"] = "Sorry the User does not Exist.";
                    return RedirectToAction(nameof(UserList));
                }
                return RedirectToAction(nameof(CompanyDetails), new { id = company.Id });
            }
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["Error"] = "Sorry, this User does not exist.";
                return RedirectToAction(nameof(UserList));
            }
            if (user.UserType == "Company")
            {
                var company = await _context.Companies.Include(u => u.User).Where(i => i.UserId == user.Id).FirstOrDefaultAsync();
                if (company !=null)
                    _context.Companies.Remove(company);              
            }
            else if (user.UserType == "Driver")
            {
                var driver = await _context.Drivers.Include(u => u.User).Where(i => i.UserId == user.Id).FirstOrDefaultAsync();
                if (driver != null)
                    _context.Drivers.Remove(driver);
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["Success"] = "The User has been deleted successfully!";
            return RedirectToAction(nameof(UserList));
        }

        // ----------------------------------------------------------------- Driver section.
        public async Task<IActionResult> DriverList()
        {
            var drivers = await _context.Drivers.Include(u => u.User).ToListAsync();
            return View(drivers);
        }

        public async Task<IActionResult> DriverDetails(int id)
        {
            var driver = await _context.Drivers.Include(u => u.User).FirstOrDefaultAsync(u => u.Id == id);
            if (driver == null)
                return NotFound();
            return View(driver);
        }

        // ----------------------------------------------------------------- Company section.
        public async Task<IActionResult> CompanyList()
        {
            var companies = await _context.Companies.Include(u => u.User).ToListAsync();
            return View(companies);
        }

        public async Task<IActionResult> CompanyDetails(int id)
        {
            var company = await _context.Companies.Include(u => u.User).FirstOrDefaultAsync(u => u.Id == id);
            if (company == null)
                return NotFound();
            return View(company);
        }

        // ----------------------------------------------------------------- Payment section.
        // GET: Paid Payments List
        public async Task<IActionResult> Payments()
        {
            try
            {
                var paidPayments = await _context.Payments
                    .Where(p => p.IsPaid)
                    .Include(p => p.User)
                    .OrderByDescending(p => p.PaymentDate)
                    .ToListAsync();

                if (!paidPayments.Any())
                {
                    ViewBag.Message = "No paid payments found.";
                }

                return View(paidPayments);
            }
            catch (Exception)
            {
                // Log the exception
                ViewBag.Error = "An error occurred while fetching paid payments.";
                return View(new List<Payment>());
            }
        }

        public async Task<IActionResult> PaymentDetails(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var payment = await _context.Payments
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == id && p.IsPaid);
                if (payment == null) return NotFound();

                return View(payment);
            }
            catch (Exception)
            {
                // Log the exception
                ViewBag.Error = "An error occurred while fetching payment details.";
                return View();
            }
        }

        // ----------------------------------------------------------------- Feedback section.
        // GET: Feedback page
        public async Task<IActionResult> Feedback()
        {
            try
            {
                var feedbackList = await _context.Feedbacks
                    .OrderByDescending(f => f.Id)  // Latest feedback first
                    .ToListAsync();

                if (!feedbackList.Any())
                {
                    ViewBag.Message = "No feedback entries found.";
                }

                return View(feedbackList);
            }
            catch (Exception)
            {
                // Log the exception
                ViewBag.Error = "An error occurred while fetching feedback.";
                return View(new List<Feedback>());
            }
        }

        // GET: Feedback Details
        public async Task<IActionResult> FeedbackDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var feedback = await _context.Feedbacks
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (feedback == null)
                {
                    return NotFound();
                }

                return View(feedback);
            }
            catch (Exception)
            {
                // Log the exception
                ViewBag.Error = "An error occurred while fetching feedback details.";
                return View();
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                TempData["Error"] = "Sorry, this feedback does not exist.";
                return RedirectToAction(nameof(Feedback));
            }
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            TempData["Success"] = "The feedback has been deleted successfully!";
            return RedirectToAction(nameof(Feedback));
        }

        // ----------------------------------------------------------------- My Profile section.
        public async Task<IActionResult> MyProfile()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "Sorry, this user does not exist.";
                return RedirectToAction(nameof(Dashboard));
            }
            return View(user);
        }
       
    }
}
