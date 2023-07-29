using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Response;
using AutoShop.Service.Interfaces;

namespace AutoShop.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCarsAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = await _carRepository.SelectAsync();
                if(!cars.Any())
                {
                    baseResponse.Description = "0 elements found";
                    baseResponse.StatusCode = StatusCode.Ok;

                    return baseResponse;
                }

                baseResponse.Data = cars;
                baseResponse.StatusCode = StatusCode.Ok;

                return baseResponse;
            }

            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                };
            }
        }
    }
}