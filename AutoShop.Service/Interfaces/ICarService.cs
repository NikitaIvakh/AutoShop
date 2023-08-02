using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Car;

namespace AutoShop.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<Car>> CreateCarAsync(CarViewModel car, byte[] imageData);

        Task<IBaseResponse<CarViewModel>> GetCarAsync(int id);

        IBaseResponse<IEnumerable<Car>> GetCars();

        IBaseResponse<IDictionary<int, string>> GetTypes();

        Task<IBaseResponse<Car>> EditCarAsync(CarViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteCarAsync(int id);
    }
}