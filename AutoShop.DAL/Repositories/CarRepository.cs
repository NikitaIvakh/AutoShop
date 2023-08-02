using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.DAL.Repositories
{
    public class CarRepository : IBaseRepository<Car>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CarRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAsync(Car entity)
        {
            await _applicationDbContext.Cars.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<Car> GetAllElements()
        {
            return _applicationDbContext.Cars;
        }

        public async Task<Car> UpdateAsync(Car type)
        {
            _applicationDbContext.Cars.Update(type);
            await _applicationDbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteAsync(Car entity)
        {
            _applicationDbContext.Cars.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}