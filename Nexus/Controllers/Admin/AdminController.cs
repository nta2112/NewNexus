﻿using Microsoft.AspNetCore.Mvc;
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
            var add = collection["Address"];
            if (string.IsNullOrEmpty(add))
            {
                ViewBag.AddError = "Address is required.";
            }
            if (!string.IsNullOrEmpty(ViewBag.AddError))
            {
                return View();
            }
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
                return RedirectToAction(nameof(TbShop));
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
                    Vendor.Name = collection["Name"];
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(TbVendor));
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
            var name = collection["Name"];
            var email = collection["Email"];
            if (string.IsNullOrEmpty(name))
            {
                ViewBag.NameError = "Name is required.";
            }

            if (string.IsNullOrEmpty(email))
            {
                ViewBag.DescriptionError = "Email is required.";
            }
            if (!string.IsNullOrEmpty(ViewBag.NameError) ||
                !string.IsNullOrEmpty(ViewBag.EmailError) )
            {
                ViewBag.VendorId = new SelectList(_context.Vendors, "VendorId", "Name");
                return View();
            }

            try
            {
                using (var db = new Models.NexusContext())
                {
                    db.Vendors.Add(new Models.Vendor
                    {
                        Email = collection["Email"],
                        Name = collection["Name"]
                    });  

                    db.SaveChanges();
                }
                return RedirectToAction(nameof(TbVendor));

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
                return RedirectToAction(nameof(TbEmployee));
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(IFormCollection collection)
        {
            try
            {
                var name = collection["Name"];
                var email = collection["Email"];
                var password = collection["Password"];
                var roleId = collection["RoleId"];
                var shopId = collection["ShopId"];

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


        public IActionResult TbPac()
        {
            var p = _context.ServicePackages.ToList();

            return View(p);
        }

        public IActionResult PacDetails(int id)
        {
            var p = _context.ServicePackages
                                   .Include(e => e.ConnectionType)
                                   .FirstOrDefault(e => e.PackageId == id);
            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        public async Task<IActionResult> EditPac(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = _context.ServicePackages
                .FirstOrDefault(m => m.PackageId == id);
            if (p == null)
            {
                return NotFound();
            }

            ViewBag.ConnectionTypeId = new SelectList(_context.ConnectionTypes, "Id", "Name", p.ConnectionTypeId);

            return View(p);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPac(int id, IFormCollection collection)
        {
            try
            {
                using (var db = new NexusContext())
                {
                    var p = db.ServicePackages.FirstOrDefault(e => e.PackageId == id);
                    if (p == null)
                    {
                        return NotFound();
                    }

                    p.Name = collection["Name"];
                    p.Description = collection["Description"];
                    p.Price = Convert.ToDecimal(collection["Price"]);
                    p.ConnectionTypeId = string.IsNullOrEmpty(collection["ConnectionTypeId"])
                        ? (int?)null
                        : Convert.ToInt32(collection["ConnectionTypeId"]);
                    p.status = collection["status"] == "true"; 

                    db.SaveChanges();
                }
                return RedirectToAction(nameof(TbPac)); 
            }
            catch
            {
                return View();
            }
        }

        public IActionResult AddPac(int? id)
        {
            ViewBag.ConnectionTypeId = new SelectList(_context.ConnectionTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPac(IFormCollection collection)
        {
            try
            {
                // Lấy các giá trị từ form
                var name = collection["Name"];
                var description = collection["Description"];
                var price = collection["Price"];
                var connectionTypeId = collection["ConnectionTypeId"];
                var status = collection["status"];

                // Kiểm tra từng trường một và thêm lỗi vào ViewBag nếu cần
                if (string.IsNullOrEmpty(name))
                {
                    ViewBag.NameError = "Name is required.";
                }
                if (string.IsNullOrEmpty(description))
                {
                    ViewBag.DescriptionError = "Description is required.";
                }
                if (string.IsNullOrEmpty(price) || !decimal.TryParse(price, out _))
                {
                    ViewBag.PriceError = "Valid Price is required.";
                }
                if (string.IsNullOrEmpty(connectionTypeId) || !int.TryParse(connectionTypeId, out _))
                {
                    ViewBag.ConnectionTypeIdError = "Invalid Connection Type.";
                }
                if (string.IsNullOrEmpty(status) || !(status == "true" || status == "false"))
                {
                    ViewBag.StatusError = "Status is required.";
                }

                // Kiểm tra có lỗi nào không
                if (ViewBag.NameError != null || ViewBag.DescriptionError != null || ViewBag.PriceError != null || ViewBag.ConnectionTypeIdError != null || ViewBag.StatusError != null)
                {
                    // Khởi tạo lại danh sách chọn
                    ViewBag.ConnectionTypeId = new SelectList(_context.ConnectionTypes, "ConnectionTypeId", "TypeName");
                    return View();
                }

                // Tạo đối tượng ServicePackage và lưu vào cơ sở dữ liệu
                using (var db = new NexusContext())
                {
                    var servicePackage = new ServicePackage
                    {
                        Name = name,
                        Description = description,
                        Price = Convert.ToDecimal(price),
                        ConnectionTypeId = Convert.ToInt32(connectionTypeId),
                        status = status == "true"
                    };

                    db.ServicePackages.Add(servicePackage);
                    db.SaveChanges();
                }

                return RedirectToAction(nameof(TbPac));
            }
            catch
            {
                //ViewBag.ConnectionTypeId = new SelectList(_context.ConnectionTypes, "ConnectionTypeId", "TypeName");
                //return View();
                return RedirectToAction(nameof(Index));
            }
        }



        [HttpGet]
		public IActionResult SearchPac(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				ViewBag.ErrorMessage = "Please enter a search term.";
				return View("SearchPac", new List<ServicePackage>());
			}

			//var results = _context.ServicePackages
			//	.Where(p => p.Name.Contains(searchTerm))
			//	.ToList();
			var results = _context.ServicePackages
			.Where(p => p.Name.Contains(searchTerm) ||
						p.Description.Contains(searchTerm) ||
						p.Price.ToString().Contains(searchTerm) ||
						p.ConnectionType.Name.Contains(searchTerm))
			.ToList();


			return View("SearchPac", results);
		}

		// Phương thức POST để xử lý tìm kiếm từ form
		[HttpPost]
		public IActionResult SearchPac(IFormCollection collection)
		{
			var searchTerm = collection["searchTerm"];
			return RedirectToAction("SearchPac", new { searchTerm = searchTerm });
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DelPac(int id)
        {
            var package = _context.ServicePackages.Find(id);
            if (package == null)
            {
                return NotFound();
            }

            if (!package.status)
            {
                TempData["Message"] = "Package is already inactive.";
            }
            else
            {
                package.status = false;
                _context.SaveChanges();
                TempData["Message"] = "Package has been set to inactive.";
            }

            return RedirectToAction(nameof(TbPac));
        }

    }
}
