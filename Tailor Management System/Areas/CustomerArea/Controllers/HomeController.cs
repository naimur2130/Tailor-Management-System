using Microsoft.AspNetCore.Mvc;

namespace Tailor_Management_System.Areas.CustomerArea.Controllers
{
    [Area("CustomerArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
