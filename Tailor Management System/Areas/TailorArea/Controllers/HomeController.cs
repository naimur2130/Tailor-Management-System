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
        [HttpGet]
        public IActionResult ConfirmForPay()
        {
            var tailorId = _userManager.GetUserId(User);

            var orders = _context.OrderAssignment
                .Include(o => o.Order)  // <-- load related Order
                .Where(o => o.EmployeeId == tailorId && o.Order.Status == OrderStatus.ConfirmedByTailor)
                .ToList();

            return View(orders);
        }
        [HttpPost]
        public IActionResult ConfirmForPay(int id,string status)
        {
            var order = _context.Order.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            order.Status = status;
            _context.SaveChanges();
            return RedirectToAction("ConfirmForPay");
        }
        [HttpGet]
        public IActionResult Inventories()
        {
            var inventories = _context.Inventory.ToList();
            return View(inventories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateInventory(int[] inventoryIds, int[] quantities)
        {
            if (inventoryIds == null || quantities == null || inventoryIds.Length != quantities.Length)
            {
                TempData["error"] = "Invalid submission!";
                return RedirectToAction("Inventories");
            }

            for (int i = 0; i < inventoryIds.Length; i++)
            {
                if (quantities[i] > 0)
                {
                    var inventory = _context.Inventory.FirstOrDefault(x => x.InventoryId == inventoryIds[i]);
                    if (inventory == null) continue;

                    if (quantities[i] > inventory.Quantity)
                    {
                        TempData["error"] = $"Not enough stock for {inventory.ItemName}!";
                        return RedirectToAction("Inventories");
                    }

                    inventory.Quantity -= quantities[i];
                    _context.Inventory.Update(inventory);
                }
            }

            _context.SaveChanges();
            TempData["success"] = "Inventory updated successfully!";
            return RedirectToAction("Inventories");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
