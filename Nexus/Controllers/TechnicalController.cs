using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace Nexus.Controllers
{
    public class TechnicalController : Controller
    {
        private readonly NexusContext _context;


        public TechnicalController(NexusContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TbProduct()
        {
            var p = _context.Products.ToList();

            return View(p);
        }

        public IActionResult AddProduct()
        {
            ViewBag.VendorId = new SelectList(_context.Vendors, "VendorId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(IFormCollection form)
        {

            var name = form["Name"];
            var description = form["Description"];
            var price = form["Price"];
            var vendorId = form["VendorId"];
            var quantity = form["Quantity"];
            var imgFile = form.Files.GetFile("img");

            // Kiểm tra tính hợp lệ của các trường
            if (string.IsNullOrEmpty(name))
            {
                ViewBag.NameError = "Name is required.";
            }

            if (string.IsNullOrEmpty(description))
            {
                ViewBag.DescriptionError = "Description is required.";
            }

            if (string.IsNullOrEmpty(price) || !decimal.TryParse(price, out var parsedPrice))
            {
                ViewBag.PriceError = "Valid price is required.";
            }

            if (string.IsNullOrEmpty(vendorId) || !int.TryParse(vendorId, out var parsedVendorId))
            {
                ViewBag.VendorIdError = "Valid vendor is required.";
            }

            if (string.IsNullOrEmpty(quantity) || !int.TryParse(quantity, out var parsedQuantity))
            {
                ViewBag.QuantityError = "Valid quantity is required.";
            }

            if (imgFile == null || imgFile.Length == 0)
            {
                ViewBag.ImageError = "Image is required.";
            }

            // Nếu có lỗi, trả lại view với thông báo lỗi
            if (!string.IsNullOrEmpty(ViewBag.NameError) ||
                !string.IsNullOrEmpty(ViewBag.DescriptionError) ||
                !string.IsNullOrEmpty(ViewBag.PriceError) ||
                !string.IsNullOrEmpty(ViewBag.VendorIdError) ||
                !string.IsNullOrEmpty(ViewBag.QuantityError) ||
                !string.IsNullOrEmpty(ViewBag.ImageError))
            {
                ViewBag.VendorId = new SelectList(_context.Vendors, "VendorId", "Name");
                return View();
            }

            var product = new Product
            {
                Name = form["Name"],
                Description = form["Description"],
                Price = decimal.Parse(form["Price"]),
                VendorId = int.Parse(form["VendorId"]),
                Quantity = int.Parse(form["Quantity"])
            };

            //var imgFile = form.Files.GetFile("img");
            if (imgFile != null && imgFile.Length > 0)
            {
                var fileName = Path.GetFileName(imgFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(stream);
                }

                product.img = "/images/" + fileName;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TbProduct));
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.Products
                                        .Include(p => p.Vendor)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var p = _context.Products
                .FirstOrDefault(m => m.ProductId == id);
            if (p == null)
            {
                return NotFound();
            }

            ViewBag.VendorId = new SelectList(_context.Vendors, "VendorId", "Name", p.VendorId);
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, IFormCollection form)
        {
            var name = form["Name"];
            var description = form["Description"];
            var price = form["Price"];
            var vendorId = form["VendorId"];
            var quantity = form["Quantity"];
            var imgFile = form.Files.GetFile("img");

            decimal parsedPrice = 0;
            int parsedVendorId = 0;
            int parsedQuantity = 0;

            // Kiểm tra tính hợp lệ của các trường
            if (string.IsNullOrEmpty(name))
            {
                ViewBag.NameError = "Name is required.";
            }

            if (string.IsNullOrEmpty(description))
            {
                ViewBag.DescriptionError = "Description is required.";
            }

            if (string.IsNullOrEmpty(price) || !decimal.TryParse(price, out parsedPrice))
            {
                ViewBag.PriceError = "Valid price is required.";
            }

            if (string.IsNullOrEmpty(vendorId) || !int.TryParse(vendorId, out parsedVendorId))
            {
                ViewBag.VendorIdError = "Valid vendor is required.";
            }

            if (string.IsNullOrEmpty(quantity) || !int.TryParse(quantity, out parsedQuantity))
            {
                ViewBag.QuantityError = "Valid quantity is required.";
            }

            // Nếu có lỗi, trả lại view với thông báo lỗi
            if (!string.IsNullOrEmpty(ViewBag.NameError) ||
                !string.IsNullOrEmpty(ViewBag.DescriptionError) ||
                !string.IsNullOrEmpty(ViewBag.PriceError) ||
                !string.IsNullOrEmpty(ViewBag.VendorIdError) ||
                !string.IsNullOrEmpty(ViewBag.QuantityError))
            {
                ViewBag.VendorId = new SelectList(_context.Vendors, "VendorId", "Name", vendorId);
                return View(await _context.Products.FindAsync(id));
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = name;
            product.Description = description;
            product.Price = parsedPrice;
            product.VendorId = parsedVendorId;
            product.Quantity = parsedQuantity;

            // Xử lý upload ảnh mới
            if (imgFile != null && imgFile.Length > 0)
            {
                var fileName = Path.GetFileName(imgFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(stream);
                }

                product.img = "/images/" + fileName;
            }
            else
            {
                // Giữ lại ảnh hiện tại nếu không có ảnh mới được upload
                product.img = form["CurrentImage"];
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(TbProduct));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }



    }
}

