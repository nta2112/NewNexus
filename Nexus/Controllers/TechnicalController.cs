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
            var product = new Product
            {
                Name = form["Name"],
                Description = form["Description"],
                Price = decimal.Parse(form["Price"]),
                VendorId = int.Parse(form["VendorId"]),
                Quantity = int.Parse(form["Quantity"])
            };

            var imgFile = form.Files.GetFile("img");
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

    }
}

