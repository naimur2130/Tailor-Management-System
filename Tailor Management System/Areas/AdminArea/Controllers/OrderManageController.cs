using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tailor_Management_System.Data;
using Tailor_Management_System.Models;
using Tailor_Management_System.ViewModel;

namespace Tailor_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class OrderManageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderManageController(ApplicationDbContext context, UserManager<IdentityUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> PendingRequests()
        {
            var order = _context.Order.Where(u => u.Status == "Pending").
                OrderBy(u=>u.CreatedAt).ToList();

            var tailors = await _userManager.GetUsersInRoleAsync("Tailor");
            ViewBag.Tailor = tailors;
            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateOrder(int id, string status, string assignedEmployeeId)
        {
            var order = _context.Order.FirstOrDefault(u => u.OrderId == id);
            if (order == null)
                return NotFound();

            if (status == OrderStatus.Rejected)
            {
                order.Status = OrderStatus.Rejected;
            }
            else if (status == OrderStatus.ConfirmedByTailor && !string.IsNullOrEmpty(assignedEmployeeId))
            {
                order.Status = OrderStatus.AssignedToTailor;

                var assignment = new OrderAssignment
                {
                    OrderId = order.OrderId,
                    EmployeeId = assignedEmployeeId
                };
                _context.OrderAssignment.Add(assignment);

                var notification = new Notification
                {
                    UserId = assignedEmployeeId,
                    Message = $"A new order (#{order.OrderId}) has been assigned to you.",
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };
                _context.Notification.Add(notification);
            }

            _context.SaveChanges();
            return RedirectToAction("PendingRequests");
        }


    }
}
