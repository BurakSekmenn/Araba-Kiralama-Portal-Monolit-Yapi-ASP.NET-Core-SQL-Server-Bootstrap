using AspNetCoreHero.ToastNotification;
using BurakSekmen.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"));
});
// Add services to the container.
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "CookieAutApp";
    opt.ExpireTimeSpan = TimeSpan.FromDays(3);
    opt.LoginPath = new PathString("/Login/Index");
    opt.LogoutPath = new PathString("/Logout/Index");
    opt.AccessDeniedPath = new PathString("/Logout/Index");
    opt.Cookie.HttpOnly = true;
    opt.SlidingExpiration = true;
});
//builder.Services.ConfigureApplicationCookie(opt =>
//{
//    opt.Cookie.Name = "AppCookie";
//    opt.LoginPath = new PathString("/Login/Index");
//    opt.LogoutPath = new PathString("/Admin/Logout");
//    opt.AccessDeniedPath = new PathString("/Admin/AccessDenied");
//    opt.Cookie.HttpOnly = true;
//    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
//    opt.SlidingExpiration = true;
//});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Add this line
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
