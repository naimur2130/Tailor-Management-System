using Microsoft.AspNetCore.Mvc;

namespace Tailor_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
