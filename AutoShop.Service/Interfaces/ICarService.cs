using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Car;

namespace AutoShop.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<Car>> CreateCarAsync(CarViewModel car, byte[] imageData);

        Task<IBaseResponse<IEnumerable<Car>>> GetCarsAsync();

        Task<IBaseResponse<CarViewModel>> GetCarAsync(int id);

        Task<IBaseResponse<Car>> GetCarByNameAsync(string name);

        Task<IBaseResponse<Car>> EditCarAsync(CarViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteCarAsync(int id);
    }
}