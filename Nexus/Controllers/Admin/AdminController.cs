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

        public IActionResult TbShop()
        {
			var shop = _context.RetailShops.ToList();
			return View(shop);
        }

		public ActionResult EditShop(int id)
		{
			Models.RetailShop s = new Models.RetailShop();
			using (var db = new Models.NexusContext())
			{
				s = db.RetailShops.Where(P => P.ShopId == id).First();
			}

			return View(s);
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditShop(int id, IFormCollection collection)
        {
            try
            {
                using (var db = new Models.NexusContext())
                {
                    var Friend = db.RetailShops.Where(P => P.ShopId == id).First();
                    Friend.Address = collection["Address"];
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(TbShop));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddShop()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShop(IFormCollection collection)
        {
            try
            {
                using (var db = new Models.NexusContext())
                {
                    db.RetailShops.Add(new Models.RetailShop
                    {
                        Address = collection["Address"]
                    });
                    
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult TbVendor()
		{
			var vendor = _context.Vendors.ToList();
			return View(vendor);
		}

        public ActionResult AddVendor()
        {
            return View();
        }

        public ActionResult EditVendor(int id)
        {
            Models.Vendor v = new Models.Vendor();
            using (var db = new Models.NexusContext())
            {
                v = db.Vendors.Where(P => P.VendorId == id).First();
            }

            return View(v);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVendor(int id, IFormCollection collection)
        {
            try
            {
                using (var db = new Models.NexusContext())
                {
                    var Vendor = db.Vendors.Where(P => P.VendorId == id).First();
                    Vendor.Email = collection["Email"];
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(TbShop));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVendor(IFormCollection collection)
        {
            try
            {
                using (var db = new Models.NexusContext())
                {
                    db.Vendors.Add(new Models.Vendor
                    {
                        Email = collection["Email"]
                    });

                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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

        public IActionResult DelEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(TbEmployee));
        }

        public IActionResult AddEmployee()
        {
            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName");
            ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address");
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddEmployee(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Employees.Add(employee);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(TbEmployee));
        //    }
        //    ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
        //    ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "ShopName", employee.ShopId);
        //    return View(employee);
        //}


        //[HttpPost]  //Why ModelState don't work in this pj?
        //[ValidateAntiForgeryToken]
        //public ActionResult AddEmployee(IFormCollection collection, Employee model)

        //{
        //    try
        //    {
        //        using (var db = new NexusContext())
        //        {
        //            var employee = new Employee
        //            {
        //                Name = collection["Name"],
        //                Email = collection["Email"],
        //                Password = collection["Password"],
        //                RoleId = Convert.ToInt32(collection["RoleId"]),
        //                ShopId = Convert.ToInt32(collection["ShopId"])
        //            };
        //            if (ModelState.IsValid)
        //            {
        //                db.Employees.Add(employee);
        //                db.SaveChanges();
        //            }


        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName");
        //        ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address");
        //        return View();
        //    }

        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(IFormCollection collection)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                var name = collection["Name"];
                var email = collection["Email"];
                var password = collection["Password"];
                var roleId = collection["RoleId"];
                var shopId = collection["ShopId"];

                // Kiểm tra từng trường một và thêm lỗi vào ViewBag nếu cần
                if (string.IsNullOrEmpty(name))
                {
                    ViewBag.NameError = "Name is required.";
                }
                if (string.IsNullOrEmpty(email))
                {
                    ViewBag.EmailError = "Email is required.";
                }
                if (string.IsNullOrEmpty(password))
                {
                    ViewBag.PasswordError = "Password is required.";
                }
                if (string.IsNullOrEmpty(roleId) || !int.TryParse(roleId, out _))
                {
                    ViewBag.RoleIdError = "Role is required.";
                }
                if (string.IsNullOrEmpty(shopId) || !int.TryParse(shopId, out _))
                {
                    ViewBag.ShopIdError = "Shop address is required.";
                }

                if (ViewBag.NameError != null || ViewBag.EmailError != null || ViewBag.PasswordError != null || ViewBag.RoleIdError != null || ViewBag.ShopIdError != null)
                {
                    ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName");
                    ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address");
                    return View();
                }

                using (var db = new NexusContext())
                {
                    var employee = new Employee
                    {
                        Name = name,
                        Email = email,
                        Password = password,
                        RoleId = Convert.ToInt32(roleId),
                        ShopId = Convert.ToInt32(shopId)
                    };

                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName");
                ViewBag.ShopId = new SelectList(_context.RetailShops, "ShopId", "Address");
                return View();
            }
        }

    }
}
