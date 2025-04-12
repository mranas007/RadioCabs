using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioCabs.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Driver Name is required.")]
        [StringLength(100, ErrorMessage = "Driver Name cannot exceed 100 characters.")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Driver ID is required.")]
        [StringLength(50, ErrorMessage = "Driver ID cannot exceed 50 characters.")]
        public string DriverID { get; set; }  // Unique

        [Required(ErrorMessage = "Contact Person is required.")]
        [StringLength(100, ErrorMessage = "Contact Person cannot exceed 100 characters.")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Experience is required.")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years.")]
        public int Experience { get; set; }  // Years of Experience

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        // Foreign key linking to the User
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}