using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tailor_Management_System.Data;

namespace Tailor_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public PaymentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Transactions()
        {
            var payments = await _context.Payment
                .Include(p => p.Order)
                .Include(p => p.User)
                .OrderByDescending(p => p.PaymentId)
                .ToListAsync();

            return View(payments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var payment = await _context.Payment
                .Include(p => p.Order)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return NotFound();

            return View(payment);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
