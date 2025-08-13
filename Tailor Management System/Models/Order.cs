using System.ComponentModel.DataAnnotations;

namespace Tailor_Management_System.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string DressType { get; set; }
        [Required]
        public string Measurements { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
        [Required]
        public string TimeDuration { get; set; }
        public bool PaymentDone { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsNotified { get; set; } = false;
    }
}
