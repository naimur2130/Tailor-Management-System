using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tailor_Management_System.Data;
using Tailor_Management_System.Models;
using Tailor_Management_System.ViewModel;

namespace Tailor_Management_System.Areas.TailorArea.Controllers
{
    [Area("TailorArea")]
    public class OrderManageController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderManageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult PendingRequests()
        {
            var order = _context.Order.Where(u => u.Status == "Pending").
                OrderBy(u=>u.CreatedAt).ToList();

            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateOrder(int id, string status)
        {
            var order  = _context.Order.FirstOrDefault(u=>u.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = status;
            order.IsNotified = false;
            _context.SaveChanges();
            return RedirectToAction("PendingRequests");
        }
        
    }
}
