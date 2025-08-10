using Microsoft.AspNetCore.Mvc;

namespace Tailor_Management_System.Areas.TailorArea.Controllers
{
    [Area("TailorArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
