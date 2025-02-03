using AbsenceManager.Data;
using AbsenceManager.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure PostgreSQL Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC Services
builder.Services.AddControllersWithViews();

// Optional: Add session support (if you plan to store session data)
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout as needed
	options.Cookie.HttpOnly = true;  // Ensure cookies are HttpOnly for security
});

// Optional: Add Authentication & Authorization (if required in the future)
builder.Services.AddAuthentication("Cookies")
	.AddCookie(options =>
	{
		options.LoginPath = "/Account/Login"; // Redirect to login page if not authenticated
	});

// Add MVC Views and Controllers (with necessary authorization support)
builder.Services.AddAuthorization();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// In Production, use Exception Handler for better security
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection(); // Redirect to HTTPS
app.UseStaticFiles();      // Serve static files (CSS, JS, images, etc.)

// Use Routing (add session middleware before routing if used)
app.UseRouting();

// Enable Session middleware (if used)
app.UseSession();

// Enable Authentication and Authorization (if added in services)
app.UseAuthentication();
app.UseAuthorization();

// Map Controller Routes
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
