using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CarRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CreateAsync(Car entity)
        {
            if (entity is not null)
            {
                await _applicationDbContext.Cars.AddAsync(entity);
                await _applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Car> GetAsync(int id)
        {
            var carId = await _applicationDbContext.Cars.FirstOrDefaultAsync(key => key.Id == id);
            return carId is null ? throw new Exception("Car with the specified id doesn't exist.") : carId;
        }

        public async Task<IEnumerable<Car>> SelectAsync()
        {
            return await _applicationDbContext.Cars.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Car entity)
        {
            if (entity is not null)
            {
                _applicationDbContext.Cars.Remove(entity);
                await _applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Car> GetByNameAsync(string name)
        {
            var car = await _applicationDbContext.Cars.FirstOrDefaultAsync(key => key.Name == name);
            return car is null ? throw new Exception("Car with the specified name doesn't exist.") : car;
        }
    }
}