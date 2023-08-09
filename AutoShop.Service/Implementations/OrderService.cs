using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Order;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Order> _orderRepository;

        public OrderService(IBaseRepository<User> userRepository, IBaseRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IBaseResponse<Order>> CreateAsync(CreateOrderViewModel createOrderViewModel)
        {
            try
            {
                var user = await _userRepository.GetAllElements().Include(key => key.Basket).FirstOrDefaultAsync(key => key.Name == createOrderViewModel.Login);
                if (user is null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "User not found",
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                    };
                }

                var order = new Order
                {
                    FirstName = createOrderViewModel.FirstName,
                    LastName = createOrderViewModel.LastName,
                    MiddleName = createOrderViewModel.MiddleName,
                    Address = createOrderViewModel.Address,
                    DateCreated = DateTime.UtcNow,
                    BasketId = user.Basket.Id,
                    CarId = createOrderViewModel.CarId,
                };

                await _orderRepository.CreateAsync(order);

                return new BaseResponse<Order>()
                {
                    Description = $"The order has been created",
                    StatusCode = Domain.Enum.StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<Order>
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var order = await _orderRepository.GetAllElements().Select(key => key.Basket.Orders.FirstOrDefault(key => key.Id == id)).FirstOrDefaultAsync();
                if (order is null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Order not found",
                        StatusCode = Domain.Enum.StatusCode.OrderNotFound,
                    };
                }

                await _orderRepository.DeleteAsync(order);

                return new BaseResponse<bool>()
                {
                    Description = $"Order deleted",
                    StatusCode = Domain.Enum.StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }
    }
}