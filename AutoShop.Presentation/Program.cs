using AutoShop.Presentation;
using Microsoft.AspNetCore;

internal class Program
{
    private static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
}