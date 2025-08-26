using System.ComponentModel.DataAnnotations;

namespace Tailor_Management_System.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Unit { get; set; }
    }
}
