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

        public bool Create(Car entity)
        {
            throw new NotImplementedException();
        }
        public Car Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> Select()
        {
            return await _applicationDbContext.Cars.ToListAsync();
        }

        public bool Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public Car GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}