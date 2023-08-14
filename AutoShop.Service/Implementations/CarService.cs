using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Extensions;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Car;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly IBaseRepository<Car> _carRepository;

        public CarService(IBaseRepository<Car> baseRepository)
        {
            _carRepository = baseRepository;
        }

        public async Task<IBaseResponse<Car>> CreateCarAsync(CarViewModel carViewModel, byte[] imageData)
        {
            try
            {
                var car = new Car()
                {
                    Name = carViewModel.Name,
                    Description = carViewModel.Description,
                    Model = carViewModel.Model,
                    Speed = carViewModel.Speed,
                    Price = carViewModel.Price,
                    DateCreate = DateTime.UtcNow,
                    TypeCar = (TypeCar)int.Parse(carViewModel.TypeCar),
                    Avatar = imageData,
                };

                await _carRepository.CreateAsync(car);

                return new BaseResponse<Car>()
                {
                    Data = car,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[CreateCarAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<CarViewModel>> GetCarAsync(long id)
        {
            try
            {
                var car = await _carRepository.GetAllElements().FirstOrDefaultAsync(key => key.Id == id);
                if (car is null)
                {
                    return new BaseResponse<CarViewModel>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound,

                    };
                }

                var data = new CarViewModel()
                {
                    Name = car.Name,
                    Description = car.Description,
                    Model = car.Model,
                    Speed = car.Speed,
                    Price = car.Price,
                    DateCreate = car.DateCreate.ToLongDateString(),
                    TypeCar = car.TypeCar.GetDisplayName(),
                    Image = car.Avatar,
                };

                return new BaseResponse<CarViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.Ok,
                };
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

        public IBaseResponse<IEnumerable<Car>> GetCars()
        {
            try
            {
                var cars = _carRepository.GetAllElements();
                if (!cars.Any())
                {
                    return new BaseResponse<IEnumerable<Car>>()
                    {
                        Description = "0 elements found",
                        StatusCode = StatusCode.CarNotFound,

                    };
                }

                return new BaseResponse<IEnumerable<Car>>()
                {
                    Data = cars,
                    StatusCode = StatusCode.Ok,
                };
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

        public IBaseResponse<IDictionary<long, string>> GetTypes()
        {
            try
            {
                var type = ((TypeCar[])Enum.GetValues(typeof(TypeCar))).ToDictionary(key => (long)key, value => value.GetDisplayName());
                if (type is null)
                {
                    return new BaseResponse<IDictionary<long, string>>()
                    {
                        Description = $"Type not found",
                        StatusCode = StatusCode.InternalServerError,
                    };
                }

                return new BaseResponse<IDictionary<long, string>>()
                {
                    Data = type,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<IDictionary<long, string>>()
                {
                    Description = $"[GetTypes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IDictionary<long, string>>> GetCarAsync(string term)
        {
            try
            {
                var cars = await _carRepository.GetAllElements().Select(key => new CarViewModel()
                {
                    Id = key.Id,
                    Name = key.Name,
                    Description = key.Description,
                    Model = key.Model,
                    Speed = key.Speed,
                    Price = key.Price,
                    DateCreate = key.DateCreate.ToLongDateString(),
                    TypeCar = key.TypeCar.GetDisplayName(),
                }).Where(key => EF.Functions.Like(key.Name, $"%{term}%")).ToDictionaryAsync(key => key.Id, value => value.Name);

                return new BaseResponse<IDictionary<long, string>>()
                {
                    Data = cars,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<IDictionary<long, string>>()
                {
                    Description = $"[GetTypes]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Car>> EditCarAsync(CarViewModel carViewModel)
        {
            try
            {
                var car = await _carRepository.GetAllElements().FirstOrDefaultAsync(key => key.Id == carViewModel.Id);
                if (car is null)
                {
                    return new BaseResponse<Car>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound,
                    };
                }

                car.Name = carViewModel.Name;
                car.Description = carViewModel.Description;
                car.Model = carViewModel.Model;
                car.Speed = carViewModel.Speed;
                car.Price = carViewModel.Price;
                car.DateCreate = DateTime.ParseExact(carViewModel.DateCreate, "yyyyMMdd HH:mm", null);

                await _carRepository.UpdateAsync(car);

                return new BaseResponse<Car>()
                {
                    Data = car,
                    StatusCode = StatusCode.Ok,
                };
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

        public async Task<IBaseResponse<bool>> DeleteCarAsync(long id)
        {
            try
            {
                var car = await _carRepository.GetAllElements().FirstOrDefaultAsync(key => key.Id == id);
                if (car is null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound,
                    };
                }

                await _carRepository.DeleteAsync(car);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.Ok,
                };
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