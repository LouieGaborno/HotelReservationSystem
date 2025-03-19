using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with correct type
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enable session and HTTP context access
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout to 30 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Enable middleware for session
app.UseSession();
app.UseRouting();
app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();
app.UseStaticFiles();

// Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); // Redirect to Login by default

app.Run();
