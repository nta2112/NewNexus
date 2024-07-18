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

        //[HttpPost]

        //public IActionResult EditEmployee(int id, [Bind("EmployeeId,Name,Email,Password,RoleId,ShopId")] Employee employee)
        //{

        //    if (ModelState.IsValid)
        //    {

        //            _context.Update(employee);
        //            _context.SaveChanges();


        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
        //    ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);

        //    return View(employee);

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditEmployee(int id, [Bind("EmployeeId,Name,Email,Password,RoleId,ShopId")] Employee employee)
        //{
        //if (id != employee.EmployeeId)
        //{
        //    return NotFound();
        //}

        //if (ModelState.IsValid)
        //{
        //    try
        //    {
        //        _context.Update(employee);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_context.Employees.Any(e => e.EmployeeId == id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(TbEmployee));
        //}

        //ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
        //ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);


        //if (ModelState.IsValid)
        //{
        //    _context.Update(employee);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        //ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
        //ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);


        //if (id != employee.EmployeeId)
        //{
        //    return NotFound();
        //}

        //if (ModelState.IsValid)
        //{

        //        _context.Update(employee);
        //        await _context.SaveChangesAsync();


        //    return RedirectToAction(nameof(Index));
        //}

        //ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
        //ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);

        //if (id != employee.EmployeeId)
        //{
        //    return NotFound();
        //}

        //if (ModelState.IsValid)
        //{
        //    try
        //    {
        //        _context.Update(employee);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_context.Employees.Any(e => e.EmployeeId == employee.EmployeeId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(TbEmployee));
        //}

        //ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
        //ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);



        //    return View(employee);
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, [Bind("EmployeeId,Name,Email,Password,RoleId,ShopId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any(e => e.EmployeeId == employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
            ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address", employee.ShopId);

            return View(employee);
        }


    }
}
