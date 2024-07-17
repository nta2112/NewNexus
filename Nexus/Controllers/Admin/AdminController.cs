using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult EmployeeDetails(int id)
        {
            var employee = _context.Employees
                                   .Include(e => e.Role)
                                   .Include(e => e.Shop)
                                   .FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

    }
}
