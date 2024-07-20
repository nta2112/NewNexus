using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
    public class AccountantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
