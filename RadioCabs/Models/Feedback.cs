
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{

    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your Name is required.")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Your City is required.")]
        public string City { get; set; }


        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("Complaint|Suggestion|Compliment|Other", ErrorMessage = "Membership Type must be either Complaint, Suggestion, Compliment, or Other")]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }
    }

}