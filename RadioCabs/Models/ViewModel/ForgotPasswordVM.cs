using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models.ViewModel
{
    public class ForgotPasswordVM
    {
        [Required]
        public string Email { get; set; }
    }
}