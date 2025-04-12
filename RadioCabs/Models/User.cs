using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RadioCabs.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }

        [Required]
        public string UserType { get; set; }  // "Driver" or "Company"

        // Add this property to fix the error  
        public ICollection<Payment> Payments { get; set; }
    }
}
