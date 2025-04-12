using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models.ViewModel
{
    public class RegisterVM
    {

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; } // "Driver" or "Company"

        [Display(Name = "Description")]
        public string? Description { get; set; }

        // ******************************** Driver Fields ******************************** //
        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Display(Name = "Address")]
        public string? DriverAddress { get; set; }

        [Display(Name = "City")]
        public string? DriverCity { get; set; }

        [Display(Name = "Experience (Years)")]
        public int? Experience { get; set; }
             

        // ******************************** Company Fields ******************************** //
        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }

        [Display(Name = "Designation")]
        public string? Designation { get; set; }

        [Display(Name = "Company Address")]
        public string? CompanyAddress { get; set; }

        [Display(Name = "Membership Type")]
        [RegularExpression("Free|Basic|Premium", ErrorMessage = "Membership Type must be either Free, Basic, or Premium")]
        public string? Membership { get; set; }
    }
}