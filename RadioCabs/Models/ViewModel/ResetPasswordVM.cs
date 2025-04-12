using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models.ViewModel
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}