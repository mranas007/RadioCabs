using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Data;
using RadioCabs.Models;
using RadioCabs.Models.ViewModel;

namespace RadioCabs.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        // Constructor
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailSender emailSender,
                                 ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _context = context;
        }

        // GET: /Account/Index page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Account/Register page
        [HttpGet]
        public IActionResult Register(string usertype)
        {
            if (string.IsNullOrEmpty(usertype))
                return RedirectToAction("Index");


            if (usertype != "Driver" && usertype != "Company")
            {
                return RedirectToAction("Index");
            }

            if (usertype == "Company")
                return View("CompanyRegister");


            if (usertype == "Driver")
                return View("DriverRegister");


            TempData["Error"] = "Something went wrong";
            return RedirectToAction("Index");
        }

        //////////////////////////////////////////////// POST: /Account/Register ////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if Email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                TempData["Error"] = "Email already exists.";
                ModelState.AddModelError("Email", "Email already exists.");
                return model.UserType == "Driver" ? View("DriverRegister", model) : View("CompanyRegister", model);
            }

            // Check if Username already exists
            var isUsernameExist = await _userManager.FindByNameAsync(model.Email);
            if (isUsernameExist != null)
            {
                TempData["Error"] = "Username already exists.";
                ModelState.AddModelError("UserName", "Username already exists.");
                return model.UserType == "Driver" ? View("DriverRegister", model) : View("CompanyRegister", model);
            }

            // Create User
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email, // Using Email as Username for simplicity
                PhoneNumber = model.ContactNumber,
                UserType = model.UserType
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                TempData["Error"] = "Something went wrong.";
                return model.UserType == "Driver" ? View("DriverRegister", model) : View("CompanyRegister", model);
            }

            // Assign Role
            await _userManager.AddToRoleAsync(user, model.UserType);

            // If user is a Driver, create a Driver profile
            if (model.UserType == "Driver")
            {
                var driver = new Driver
                {
                    DriverName = model.FullName,
                    DriverID = Guid.NewGuid().ToString(), // Generate a unique ID
                    ContactPerson = model.FullName,
                    Address = model.DriverAddress,
                    City = model.DriverCity,
                    Experience = model.Experience ?? 0,
                    Description = model.Description,
                    UserId = user.Id
                };
                _context.Drivers.Add(driver);
            }

            // If user is a Company, create a Company profile
            else if (model.UserType == "Company")
            {
                var company = new Company
                {
                    CompanyName = model.CompanyName,
                    CompanyID = DateTime.Now.Ticks, // Generate a unique Company ID
                    Designation = model.Designation,
                    Address = model.CompanyAddress,
                    Membership = model.Membership,
                    Description = model.Description,
                    UserId = user.Id
                };
                _context.Companies.Add(company);
            }

            await _context.SaveChangesAsync();

            // Email Confirmation Process
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);
            var htmlMessage = $@"<table style='max-width:600px; margin: auto; border-collapse: collapse; font-family: Arial, sans-serif;'>
                                    <tr>
                                        <td style='background-color: #007bff; padding: 20px; text-align: center; color: white;'>
                                            <h2>Welcome to RadioCabs!</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 30px; background-color: #f9f9f9;'>
                                            <p style='font-size: 16px; color: #333;'>Hi there,</p>
                                            <p style='font-size: 16px; color: #333;'>
                                                Thank you for registering with <strong>RadioCabs</strong>. To complete your registration and activate your account, please confirm your email address by clicking the button below:
                                            </p>
                                            <p style='text-align: center; margin: 30px 0;'>
                                                <a href='{confirmationLink}' style='background-color: #007bff; color: white; text-decoration: none; padding: 12px 20px; border-radius: 5px; display: inline-block;'>
                                                    Confirm Email
                                                </a>
                                            </p>
                                            <p style='font-size: 14px; color: #555;'>
                                                If you did not create an account, please ignore this email.
                                            </p>
                                            <p style='font-size: 14px; color: #555;'>– The RadioCabs Team</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='text-align: center; font-size: 12px; color: #999; padding: 20px;'>
                                            &copy; {DateTime.Now.Year} RadioCabs. All rights reserved.
                                        </td>
                                    </tr>
                                </table>";

            await _emailSender.SendEmailAsync(user.Email, "Confirm Your Email", htmlMessage);

            TempData["Message"] = "Registration successful! Please check your email to confirm.";
            return View("RegisterConfirmAlert");
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["Success"] = "Email confirmed successfully. You can now log in!";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Email confirmation failed. Please contact support.";
            return View("Error");
        }

        // GET: /Account/Login page
        public async Task<IActionResult> Login()
        {
            return View();
        }

        //////////////////////////////////////////////// POST: /Account/Login ////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Ensure Email and Password are provided
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                TempData["Error"] = "Invalid login attempt.";
                ModelState.AddModelError("Email", "Invalid Email.");
                ModelState.AddModelError("Password", "Invalid Password.");
                return View(model);
            }

            // Find User by Email (since we store Email as Username)
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid login attempt.";
                ModelState.AddModelError("Email", "Invalid Email.");
                return View(model);
            }

            // Ensure Email is Confirmed before allowing login
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("Email", "Please confirm your email before logging in.");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                // Send Email Confirmation Link
                var htmlMessage = $@"<table style='max-width:600px; margin: auto; border-collapse: collapse; font-family: Arial, sans-serif;'>
                                    <tr>
                                        <td style='background-color: #007bff; padding: 20px; text-align: center; color: white;'>
                                            <h2>Welcome to RadioCabs!</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 30px; background-color: #f9f9f9;'>
                                            <p style='font-size: 16px; color: #333;'>Hi there,</p>
                                            <p style='font-size: 16px; color: #333;'>
                                                Thank you for registering with <strong>RadioCabs</strong>. To complete your registration and activate your account, please confirm your email address by clicking the button below:
                                            </p>
                                            <p style='text-align: center; margin: 30px 0;'>
                                                <a href='{confirmationLink}' style='background-color: #007bff; color: white; text-decoration: none; padding: 12px 20px; border-radius: 5px; display: inline-block;'>
                                                    Confirm Email
                                                </a>
                                            </p>
                                            <p style='font-size: 14px; color: #555;'>
                                                If you did not create an account, please ignore this email.
                                            </p>
                                            <p style='font-size: 14px; color: #555;'>– The RadioCabs Team</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='text-align: center; font-size: 12px; color: #999; padding: 20px;'>
                                            &copy; {DateTime.Now.Year} RadioCabs. All rights reserved.
                                        </td>
                                    </tr>
                                </table>";
                await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email", htmlMessage);

                TempData["Message"] = "Please check your email to confirm your account.";
                return View("RegisterConfirmAlert");
            }

            // Ensure Previous Session is Cleared
            await _signInManager.SignOutAsync();

            // Attempt Login
            var result = await _signInManager.PasswordSignInAsync(user.Email!, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userRole = await _userManager.GetRolesAsync(user);

                // Redirect based on Role
                if (userRole.Contains("Driver"))
                {
                    return RedirectToAction("Dashboard", "Driver"); // Ensure Driver Dashboard exists
                }

                else if (userRole.Contains("Company"))
                {
                    return RedirectToAction("Dashboard", "Company"); // Ensure Company Dashboard exists
                }

                return RedirectToAction("Index", "Home");
            }

            // If login fails, show error
            TempData["Error"] = "Invalid login attempt.";
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //////////////////////////////////////////////// POST: /Account/ForgotPassword ////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // check if user is exist and confirmed email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid email address.";
                return View(model);
            }

            // Ensure Email is Confirmed before allowing login
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("Email", "Please confirm your email before logging in.");
                var EmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, EmailToken }, Request.Scheme);

                // Send Email Confirmation Link
                var htmlMessage = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>";
                await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email", htmlMessage);

                TempData["Message"] = "Please check your email to confirm your account.";
                return View("ConfirmationAlert");
            }

            // create the token for reset password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { token = token, email = model.Email }, Request.Scheme);

            // send email for password reset confirmation 
            var HtmlMessage = $"Please reset your password by clicking <a href='{resetLink}'>here</a>";
            var subject = "Reset Your Password";
            await _emailSender.SendEmailAsync(model.Email, subject, HtmlMessage);

            // message for password reset link sent
            TempData["Message"] = "Password reset link has been sent to your email.";
            ViewData["Message"] = "Password reset link has been sent to your email.";
            return View("ForgotPasswordConfirmAlert");
        }

        // GET: /Account/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return BadRequest("Invalid password reset request.");
            }

            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        //////////////////////////////////////////////// POST: /Account/ResetPassword ////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid email address.";
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData["Message"] = "Password has been reset successfully.";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            TempData["Error"] = "Something went wrong.";
            return View(model);
        }

        // GET: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
