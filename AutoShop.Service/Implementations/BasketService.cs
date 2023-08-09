using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Extensions;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Order;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Car> _carRepository;

        public BasketService(IBaseRepository<User> userRepository, IBaseRepository<Car> carRepository)
        {
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItemsAsync(string userName)
        {
            try
            {
                var user = await _userRepository.GetAllElements()
                    .Include(key => key.Basket)
                    .ThenInclude(key => key.Orders)
                    .FirstOrDefaultAsync(key => key.Name == userName);

                if (user is null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = $"User not found",
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                    };
                };

                var orders = user.Basket?.Orders;
                var response = from order in orders
                               join car in _carRepository.GetAllElements() on order.CarId equals car.Id
                               select new OrderViewModel
                               {
                                   Id = order.Id,
                                   CarName = car.Name,
                                   Speed = car.Speed,
                                   TypeCar = car.TypeCar.GetDisplayName(),
                                   Model = car.Model,
                               };

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = Domain.Enum.StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItemAsync(long id, string userName)
        {
            try
            {
                var user = await _userRepository.GetAllElements()
                    .Include(key => key.Basket)
                    .ThenInclude(key => key.Orders)
                    .FirstOrDefaultAsync(key => key.Name == userName);

                if (user is null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = $"User not found",
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                    };
                }

                var orders = user.Basket?.Orders.Where(key => key.Id == id).ToList();
                if (orders is null || orders.Count == 0)
                {
                    return new BaseResponse<OrderViewModel>
                    {
                        Description = $"There are no orders",
                        StatusCode = Domain.Enum.StatusCode.OrderNotFound,
                    };
                }

                var response = (from order in orders
                                join car in _carRepository.GetAllElements() on order.Id equals car.Id
                                select new OrderViewModel
                                {
                                    Id = order.Id,
                                    CarName = car.Name,
                                    Speed = car.Speed,
                                    TypeCar = car.TypeCar.GetDisplayName(),
                                    Model = car.Model,
                                    Address = order.Address,
                                    FirstName = order.FirstName,
                                    LastName = order.LastName,
                                    MiddleName = order.MiddleName,
                                    DateCreate = order.DateCreated.ToLongDateString(),
                                }).FirstOrDefault();

                if (response is null)
                {
                    return new BaseResponse<OrderViewModel>
                    {
                        Description = $"There are no orders",
                        StatusCode = Domain.Enum.StatusCode.OrderNotFound,
                    };
                }

                return new BaseResponse<OrderViewModel>
                {
                    Data = response,
                    StatusCode = Domain.Enum.StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }
    }
}