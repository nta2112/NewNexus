using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> EditEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees
                .FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
            ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);

            return View(employee);           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(int id, IFormCollection collection)
        {
            try
            {
                using (var db = new NexusContext())
                {
                    var employee = db.Employees.FirstOrDefault(e => e.EmployeeId == id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    employee.Name = collection["Name"];
                    employee.Email = collection["Email"];
                    employee.Password = collection["Password"];
                    employee.RoleId = Convert.ToInt32(collection["RoleId"]);
                    employee.ShopId = Convert.ToInt32(collection["ShopId"]);

                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
