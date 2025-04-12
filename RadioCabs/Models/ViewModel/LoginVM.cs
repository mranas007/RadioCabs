using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models.ViewModel
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


    }
}
