using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioCabs.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 10000, ErrorMessage = "Amount must be between 1 and 10,000.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment Type is required.")]
        [RegularExpression("Monthly|Quarterly", ErrorMessage = "Payment Type must be either Monthly or Quarterly.")]
        public string? PaymentType { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public bool IsPaid { get; set; } = false;

        [StringLength(100)]
        public string? TransactionId { get; set; }

        [Required]
        [StringLength(19, ErrorMessage = "Card number must be 16 digits.")]
        [Display(Name = "Card Number")]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$", ErrorMessage = "Enter a valid format like: 1234 5678 9012 3456")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public void SetExpiryDate()
        {
            ExpiryDate = PaymentType == "Monthly" ? PaymentDate.AddMonths(1) : PaymentDate.AddMonths(3);
        }

    }

}
