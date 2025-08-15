using System.ComponentModel.DataAnnotations;

namespace Tailor_Management_System.ViewModel
{
    public class PaymentViewModel
    {
        public int OrderId { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
    }
}
