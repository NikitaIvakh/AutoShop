using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Car;
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

        public async Task<IBaseResponse<CarViewModel>> CreateCarAsync(CarViewModel carViewModel)
        {
            var baseResponse = new BaseResponse<CarViewModel>();
            try
            {
                var car = new Car()
                {
                    Name = carViewModel.Name,
                    Description = carViewModel.Description,
                    Model = carViewModel.Model,
                    Speed = carViewModel.Speed,
                    Price = carViewModel.Price,
                    DateCreate = carViewModel.DateCreate,
                    TypeCar = (TypeCar)int.Parse(carViewModel.TypeCar),
                };

                await _carRepository.CreateAsync(car);
                baseResponse.StatusCode = StatusCode.Ok;

                return baseResponse;
            }

            catch (Exception ex)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = $"[CreateCarAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCarsAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = await _carRepository.SelectAsync();
                if (!cars.Any())
                {
                    baseResponse.Description = "0 elements found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;

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
                    Description = $"[GetCarsAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<CarViewModel>> GetCarAsync(int id)
        {
            var baseResponse = new BaseResponse<CarViewModel>();
            try
            {
                var car = await _carRepository.GetAsync(id);
                if (car is null)
                {
                    baseResponse.Description = "Car not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;

                    return baseResponse;
                }

                var data = new CarViewModel()
                {
                    Name = car.Name,
                    Description = car.Description,
                    Speed = car.Speed,
                    Model = car.Model,
                    Price = car.Price,
                    DateCreate = car.DateCreate,
                    TypeCar = car.TypeCar.ToString(),
                };

                baseResponse.Data = data;
                baseResponse.StatusCode = StatusCode.Ok;

                return baseResponse;
            }

            catch (Exception ex)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = $"[GetCarAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByNameAsync(string name)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetByNameAsync(name);
                if (car is null)
                {
                    baseResponse.Description = "Car not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;

                    return baseResponse;
                }

                baseResponse.Data = car;
                baseResponse.StatusCode = StatusCode.Ok;

                return baseResponse;
            }

            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCarByNameAsync]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Car>> EditCarAsync(CarViewModel carViewModel)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetAsync(carViewModel.Id);
                if (car is null)
                {
                    baseResponse.Description = "Car not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;

                    return baseResponse;
                }

                car.Name = carViewModel.Name;
                car.Description = carViewModel.Description;
                car.Model = carViewModel.Model;
                car.Speed = carViewModel.Speed;
                car.Price = carViewModel.Price;
                car.DateCreate = carViewModel.DateCreate;

                // car.TypeCar = carViewModel.TypeCar;
                await _carRepository.UpdateElementAsync(car);

                return baseResponse;
            }

            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[EditCarAsync]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCarAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var car = await _carRepository.GetAsync(id);
                if (car is null)
                {
                    baseResponse.Description = "Car not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;

                    return baseResponse;
                }

                await _carRepository.DeleteAsync(car);
                baseResponse.StatusCode = StatusCode.Ok;

                return baseResponse;
            }

            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCarAsync]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}