using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Car;

namespace AutoShop.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<CarViewModel>> CreateCarAsync(CarViewModel car);

        Task<IBaseResponse<IEnumerable<Car>>> GetCarsAsync();

        Task<IBaseResponse<Car>> GetCarAsync(int id);

        Task<IBaseResponse<Car>> GetCarByNameAsync(string name);

        Task<IBaseResponse<bool>> DeleteCarAsync(int id);
    }
}