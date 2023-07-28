using AutoShop.Domain.Entity;

namespace AutoShop.DAL.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Car GetByName(string name);
    }
}