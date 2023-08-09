using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Car;

namespace AutoShop.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<Car>> CreateCarAsync(CarViewModel car, byte[] imageData);

        Task<IBaseResponse<CarViewModel>> GetCarAsync(long id);

        IBaseResponse<IEnumerable<Car>> GetCars();

        IBaseResponse<IDictionary<long, string>> GetTypes();

        Task<IBaseResponse<IDictionary<long, string>>> GetCarAsync(string term);

        Task<IBaseResponse<Car>> EditCarAsync(CarViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteCarAsync(long id);
    }
}