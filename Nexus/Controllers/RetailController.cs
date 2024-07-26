using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
	public class RetailController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
