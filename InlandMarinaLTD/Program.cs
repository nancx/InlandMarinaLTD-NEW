using InlandMarinaClasses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<InlandMarinaContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("databaseConnection")));

// Use builder.Services.AddAuthentication here instead of services.AddAuthentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "MyCookieAuthenticationScheme";
    options.DefaultSignInScheme = "MyCookieAuthenticationScheme";
    options.DefaultChallengeScheme = "MyCookieAuthenticationScheme";
}).AddCookie("MyCookieAuthenticationScheme", options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // UseAuthentication should come before UseAuthorization

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
