using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HotelReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        // Redirect to Login if no session
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction("Dashboard");
        }

        // Dashboard Redirection Based on Role
        public IActionResult Dashboard()
        {
            string role = HttpContext.Session.GetString("UserRole");

            return role switch
            {
                "Admin" => RedirectToAction("AdminDashboard"),
                "Staff" => RedirectToAction("StaffDashboard"),
                "Guest" => RedirectToAction("GuestDashboard"),
                _ => RedirectToAction("Index"),
            };
        }

        // ----------------------
        // ADMIN ACTIONS
        // ----------------------

        // Admin Dashboard
        public IActionResult AdminDashboard()
        {
            if (!IsAuthenticated("Admin")) return RedirectToAction("Login", "Auth");
            return View(); // Views/Home/AdminDashboard.cshtml
        }

        // Manage Guest Details & Stay Duration
        public IActionResult ManageGuests()
        {
            if (!IsAuthenticated("Admin")) return RedirectToAction("Login", "Auth");
            return View(); // Views/Home/ManageGuests.cshtml
        }

        // Edit Room Availability & Details
        public IActionResult ManageRooms()
        {
            if (!IsAuthenticated("Admin")) return RedirectToAction("Login", "Auth");
            return View(); // Views/Home/ManageRooms.cshtml
        }

        // View Payment Details
        public IActionResult PaymentDetails()
        {
            if (!IsAuthenticated("Admin")) return RedirectToAction("Login", "Auth");
            return View(); // Views/Home/PaymentDetails.cshtml
        }

        // ----------------------
        // STAFF ACTIONS
        // ----------------------

        // Staff Dashboard
        public IActionResult StaffDashboard()
        {
            if (!IsAuthenticated("Staff")) return RedirectToAction("Login", "Auth");
            return View(); // Views/Home/StaffDashboard.cshtml
        }

        // ----------------------
        // GUEST ACTIONS
        // ----------------------

        // Guest Dashboard
        public IActionResult GuestDashboard()
        {
            if (!IsAuthenticated("Guest")) return RedirectToAction("Login", "Auth");
            return View(); // Views/Home/GuestDashboard.cshtml
        }

        // ----------------------
        // LOGOUT & SESSION CHECK
        // ----------------------

        // Logout - Clear Session and Redirect
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session
            return RedirectToAction("Index", "Login");
        }

        // Utility Method to Check Authentication by Role
        private bool IsAuthenticated(string role)
        {
            string userRole = HttpContext.Session.GetString("UserRole");
            return !string.IsNullOrEmpty(userRole) && userRole == role;
        }
    }
}
