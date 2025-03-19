using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View("~/Views/Login/Index.cshtml");
        }


        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }

            // Store user info in session
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.Name);

            // Redirect to the correct dashboard based on the role
            switch (user.Role)
            {
                case "Admin":
                    return RedirectToAction("AdminDashboard", "Home");
                case "Staff":
                    return RedirectToAction("StaffDashboard", "Home");
                case "Guest":
                    return RedirectToAction("GuestDashboard", "Home");
                default:
                    return RedirectToAction("Login");
            }
        }

        // POST: /Auth/Register (inside Login page)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the email already exists
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View("Login");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Account created successfully! Please log in.";
                return RedirectToAction("Login");
            }
            return View("Login");
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
