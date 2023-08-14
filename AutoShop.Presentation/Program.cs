using AutoShop.DAL;
using AutoShop.Presentation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Logging.ClearProviders();
applicationBuilder.Logging.SetMinimumLevel(LogLevel.Trace);
applicationBuilder.Host.UseNLog();

applicationBuilder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

applicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(applicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
});

applicationBuilder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.AccessDeniedPath = new PathString("/Account/Login");
});

applicationBuilder.Services.InitializeRepositories();
applicationBuilder.Services.InitializeServices();

WebApplication webApplication = applicationBuilder.Build();

if (!webApplication.Environment.IsDevelopment())
{
    webApplication.UseExceptionHandler("/Home/Error");
    webApplication.UseHsts();
}

webApplication.UseHttpsRedirection();
webApplication.UseStaticFiles();

webApplication.UseRouting();

webApplication.UseAuthentication();
webApplication.UseAuthorization();

webApplication.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
webApplication.Run();