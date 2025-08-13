using System.ComponentModel.DataAnnotations;

namespace Tailor_Management_System.ViewModel
{
    public class PlaceOrderViewModel
    {
        [Required]
        public string DressType { get; set; }

        [Required]
        public string Measurements { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string TimeDuration { get; set; }
    }
}
