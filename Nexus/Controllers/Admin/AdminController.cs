using Microsoft.AspNetCore.Mvc;
using Nexus.Models;


namespace Nexus.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly NexusContext _context;
        public AdminController(NexusContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult TbEmployee()
		{
			var employees = _context.Employees.ToList();

			return View(employees);
		}
	}
}
