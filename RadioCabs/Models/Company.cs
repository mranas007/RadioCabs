using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioCabs.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; } // Id

        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public string CompanyName { get; set; } // Company Name

        [Required(ErrorMessage = "Company ID is required.")]
        public long CompanyID { get; set; }  // CompanyID - unique

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(50, ErrorMessage = "Designation cannot exceed 50 characters.")]
        public string Designation { get; set; } // Designation

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; } // Address

        [Required(ErrorMessage = "Membership Type is required.")]
        [RegularExpression("Free|Basic|Premium", ErrorMessage = "Membership Type must be either Free, Basic, or Premium")]
        public string Membership { get; set; } // Membership

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
        public int? PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        [Required]
        public string UserId { get; set; }
        // Foreign Keys
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();

    }

}
