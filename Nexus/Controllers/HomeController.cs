using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Models;
using System.Diagnostics;

namespace Nexus.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly NexusContext _context;
        public HomeController(NexusContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

           
            var user = await _context.Employees
                                     .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);

            if (user != null)
            {
                
                switch (user.RoleId)
                {
                    case 1:
                        return RedirectToAction("Index", "Admin");
                    case 2: 
                        return RedirectToAction("Index", "Accountant");
                    case 3:
                        return RedirectToAction("Index", "RetailEmployee");
                    case 4:
                        return RedirectToAction("Index", "Technical");
                    default:
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View();
        }

    }
}
