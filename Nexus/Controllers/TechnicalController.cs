using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
    public class TechnicalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
