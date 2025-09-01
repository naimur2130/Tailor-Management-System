using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tailor_Management_System.Data;
using Tailor_Management_System.Models;

namespace Tailor_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var proccessedOrders = _context.Order
        .Where(o => o.Status == OrderStatus.ConfirmedByTailor || 
        o.Status==OrderStatus.Rejected || o.Status=="Paid")
        .OrderByDescending(o => o.CreatedAt)
        .ToList();
            return View(proccessedOrders);
        }
    }
}
