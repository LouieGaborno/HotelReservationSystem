using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // This will use the view located at /Views/Login/Index.cshtml
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserName", user.Name);
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.ErrorMessage = "Invalid email or password";
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("Index", "Login");
            }
            return View("Index", user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // Explicitly redirect to LoginController's Index action.
            return RedirectToAction("Index", "Login");
        }
    }
}
