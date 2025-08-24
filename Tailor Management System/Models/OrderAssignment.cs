using Microsoft.AspNetCore.Identity;
using Tailor_Management_System.Models;

public class OrderAssignment
{
    public int OrderAssignmentId { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public string EmployeeId { get; set; }
    public IdentityUser Employee { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.Now;
}
