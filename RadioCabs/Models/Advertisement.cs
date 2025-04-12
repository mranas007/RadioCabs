using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioCabs.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(50)]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Enter a valid mobile number.")]
        public string Mobile { get; set; }

        [Phone(ErrorMessage = "Enter a valid telephone number.")]
        public string? Telephone { get; set; }

        [Phone(ErrorMessage = "Enter a valid fax number.")]
        public string? FaxNumber { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
    }
}
