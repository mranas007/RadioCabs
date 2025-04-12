using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models.ViewModel
{
    public class CompanyEditVM
    {

        [Key]
        public int Id { get; set; } // Id

        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public string CompanyName { get; set; } // Company Name

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(50, ErrorMessage = "Designation cannot exceed 50 characters.")]
        public string Designation { get; set; } // Designation

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; } // Address


    }

}
