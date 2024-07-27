using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Models;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Nexus.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly NexusContext _context;
        private readonly IConfiguration _configuration;
        public HomeController(NexusContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        private string GenerateJwtToken(string email, int roleId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(ClaimTypes.Role, roleId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                var token = GenerateJwtToken(user.Email, user.RoleId);
                Response.Cookies.Append("jwt", token, new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Strict });


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
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Login", "Home");
        }
    }
}
