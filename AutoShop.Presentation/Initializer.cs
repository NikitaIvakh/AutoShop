using AutoShop.DAL.Interfaces;
using AutoShop.DAL.Repositories;
using AutoShop.Domain.Entity;
using AutoShop.Service.Implementations;
using AutoShop.Service.Interfaces;

namespace Automarket
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Car>, CarRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}