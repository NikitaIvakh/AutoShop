using AutoShop.Domain.Entity;

namespace AutoShop.DAL.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car> GetByNameAsync(string name);
    }
}