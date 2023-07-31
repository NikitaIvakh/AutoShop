using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
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
                    DateCreate = carViewModel.DateCreate,
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

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCarsAsync()
        {
            try
            {
                var cars = _carRepository.GetAllAsync();
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

        public async Task<IBaseResponse<CarViewModel>> GetCarAsync(int id)
        {
            try
            {
                var car = await _carRepository.GetAllAsync().FirstOrDefaultAsync(key => key.Id == id);
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
                    Description = car.Description,
                    Model = car.Model,
                    Speed = car.Speed,
                    Price = car.Price,
                    DateCreate = car.DateCreate,
                    TypeCar = car.TypeCar.ToString(),
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

        public async Task<IBaseResponse<Car>> GetCarByNameAsync(string name)
        {
            try
            {
                var car = await _carRepository.GetAllAsync().FirstOrDefaultAsync(key => key.Name == name);
                if (car is null)
                {
                    return new BaseResponse<Car>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound,
                    };
                }

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
                    Description = $"[GetCarByNameAsync]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Car>> EditCarAsync(CarViewModel carViewModel)
        {
            try
            {
                var car = await _carRepository.GetAllAsync().FirstOrDefaultAsync(key => key.Id == carViewModel.Id);
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
                car.DateCreate = carViewModel.DateCreate;

                // car.TypeCar = carViewModel.TypeCar;
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

        public async Task<IBaseResponse<bool>> DeleteCarAsync(int id)
        {
            try
            {
                var car = await _carRepository.GetAllAsync().FirstOrDefaultAsync(key => key.Id == id);
                if (car is null)
                {
                    return new BaseResponse<bool>()
                    {
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