using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tailor_Management_System.Data;
using Tailor_Management_System.Models;
using Tailor_Management_System.ViewModel;

namespace Tailor_Management_System.Areas.CustomerArea.Controllers
{
    [Area("CustomerArea")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(ApplicationDbContext context
            , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user  = await _userManager.GetUserAsync(User);
            var order = new Order
            {
                UserId = user.Id,
                DressType = model.DressType,
                Measurements = model.Measurements,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                TimeDuration = model.TimeDuration,
                IsNotified = true
            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Your order has been placed successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MyOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var order = _context.Order.Where(u=>u.UserId == user.Id)
                .OrderBy(u => u.CreatedAt).
                ToList();
            return new JsonResult(order);
        }
        [HttpGet]
        public IActionResult Pay(int id)
        {
            var model = new PaymentViewModel
            {
                OrderId = id
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>  Pay(PaymentViewModel order)
        {
            if(ModelState.IsValid)
            {
                var existingOrder = _context.Order.Find(order.OrderId);
                var user =await _userManager.GetUserAsync(User);
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    PhoneNumber = order.PhoneNumber,
                    Amount = order.Amount,
                    UserId = user.Id
                };
                if (existingOrder != null)
                {
                    existingOrder.PaymentDone = true;
                    existingOrder.Status = "Paid";
                    _context.SaveChanges();
                    TempData["Success"] = "Payment successful!";
                    _context.Payment.Add(payment);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Order not found!";
            }
            return View(order);
        }
    }
}
