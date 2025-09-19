using CarLoanManagement.Services;
using CarLoanManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Registers Razor Pages framework for server-side rendering
builder.Services.AddRazorPages();

// Configure authentication middleware
// Uses cookie-based authentication for session management
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Redirect unauthorized users to login page
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";

        // Configure cookie settings for security
        options.Cookie.HttpOnly = true; // Prevent XSS attacks
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.SlidingExpiration = true; // Extend session on activity
        options.ExpireTimeSpan = TimeSpan.FromHours(2); // Session timeout
    });

// Register application services using Dependency Injection
// Demonstrates Dependency Inversion Principle - depend on interfaces, not implementations
// Scoped lifetime ensures one instance per request for data consistency
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ILoanService, LoanService>();

// Build the application with configured services
var app = builder.Build();

// Configure the HTTP request pipeline
// Order matters - middleware executes in the order it's added

if (!app.Environment.IsDevelopment())
{
    // Production error handling
    app.UseExceptionHandler("/Error");

    // Add HTTP Strict Transport Security (HSTS) for security
    app.UseHsts();
}

// Security middleware - redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files (CSS, JavaScript, images)
app.UseStaticFiles();

// Enable routing to map URLs to pages
app.UseRouting();

// Enable authentication middleware (must be after routing, before authorization)
app.UseAuthentication();

// Enable authorization middleware (must be after authentication)
app.UseAuthorization();

// Map Razor Pages to handle requests
app.MapRazorPages();

// Start the application and listen for requests
app.Run();