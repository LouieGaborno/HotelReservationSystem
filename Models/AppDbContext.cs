using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Parameterless constructor (if needed)
        public AppDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use SQLite with default connection string if not configured
                optionsBuilder.UseSqlite("Data Source=hotel.db");
            }
        }
    }
}
