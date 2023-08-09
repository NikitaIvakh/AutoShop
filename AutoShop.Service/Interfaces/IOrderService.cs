using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Order;

namespace AutoShop.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> CreateAsync(CreateOrderViewModel createOrderViewModel);

        Task<IBaseResponse<bool>> DeleteAsync(long id);
    }
}