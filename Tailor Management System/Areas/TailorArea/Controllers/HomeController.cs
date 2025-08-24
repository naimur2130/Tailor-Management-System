using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tailor_Management_System.Data;
using Tailor_Management_System.Models;

namespace Tailor_Management_System.Areas.TailorArea.Controllers
{
    [Area("TailorArea")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> MyAssignedOrders()
        {
            var tailorId = _userManager.GetUserId(User);

            var orders = await _context.OrderAssignment
                .Where(a => a.EmployeeId == tailorId
                    && (a.Order.Status == OrderStatus.Pending
                     || a.Order.Status == OrderStatus.AssignedToTailor))
                .Select(a => a.Order)
                .ToListAsync();

            return View(orders);
        }


        [HttpPost]
        public IActionResult ConfirmOrder(int id, string decision)
        {
            var order = _context.Order.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            if (decision == "accept")
            {
                order.Status = OrderStatus.ConfirmedByTailor;
            }
            else if (decision == "reject")
            {
                order.Status = OrderStatus.Rejected;
            }

            _context.SaveChanges();
            return RedirectToAction("MyAssignedOrders");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
