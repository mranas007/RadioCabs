using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RadioCabs.Models;
using RadioCabs.Data;
using RadioCabs.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
namespace RadioCabs.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public PaymentController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Subscribe()
        {
            // Check if user already has an active subscription
            var userId = _userManager.GetUserId(User);
            var userRole = _userManager.GetRolesAsync(_userManager.GetUserAsync(User).Result).Result.FirstOrDefault();
            var activePayment = _context.Payments
                .Where(p => p.UserId == userId && p.IsPaid)
                .OrderByDescending(p => p.ExpiryDate)
                .FirstOrDefault();

            if (activePayment != null && activePayment.ExpiryDate > DateTime.Now)
            {
                TempData["Message"] = "You already have an active subscription!";
                return RedirectToAction("Subscription", userRole);
            }

            return View(new PaymentVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe(PaymentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Check if user already has an active subscription
            var activePayment = _context.Payments
                .Where(p => p.UserId == userId && p.IsPaid)
                .OrderByDescending(p => p.ExpiryDate)
                .FirstOrDefault();

            if (activePayment != null && activePayment.ExpiryDate > DateTime.Now)
            {
                TempData["Error"] = "You already have an active subscription!";
                return RedirectToAction("Subscription", "Payments");
            }

            // Calculate Expiry Date based on Payment Type
            DateTime expiryDate = model.PaymentType == "Monthly"
                ? DateTime.Now.AddMonths(1)
                : DateTime.Now.AddMonths(3);

            // Save payment details
            var payment = new Payment
            {
                Amount = model.Amount,
                PaymentType = model.PaymentType,
                PaymentDate = DateTime.Now,
                ExpiryDate = expiryDate,
                IsPaid = true,
                CardNumber = model.CardNumber,
                TransactionId = Guid.NewGuid().ToString(),
                UserId = userId
            };

            _context.Payments.Add(payment);

            // Update user's subscription in the Company table
            var company = _context.Companies.FirstOrDefault(c => c.UserId == userId);
            if (company != null)
            {
                company.Membership = model.PaymentType == "Monthly" ? "Basic" : "Premium";
            }
            _context.SaveChanges();

            TempData["Success"] = "Payment successful! Your subscription is now active.";
            return RedirectToAction("Subscription", "Payment");
        }


        public IActionResult Subscription()
        {
            var userId = _userManager.GetUserId(User);
            var payment = _context.Payments.FirstOrDefault(p => p.UserId == userId && p.IsPaid);

            var model = new SubscriptionVM
            {
                IsSubscribed = payment != null,
                CurrentPlan = payment?.PaymentType == "Monthly" ? "Basic" : "Premium",
                LastPaymentDate = payment?.PaymentDate.ToString("dd/MM/yyyy")
            };

            return View(model);
        }
    }
}