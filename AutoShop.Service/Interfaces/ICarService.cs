using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;

namespace AutoShop.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCarsAsync();
    }
}