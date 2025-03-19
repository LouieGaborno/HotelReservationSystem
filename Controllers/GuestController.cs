using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Controllers
{
    public class GuestController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject the database context
        public GuestController(AppDbContext context)
        {
            _context = context;
        }

        // Action to manage and display guest bookings
        public IActionResult ManageGuests()
        {
            // Retrieve all bookings with related User and Room data
            var bookings = _context.Bookings
                .Include(b => b.User) // Include guest details
                .Include(b => b.Room) // Include room details
                .ToList();

            // Pass the bookings list to the view
            return View(bookings);
        }
    }
}
