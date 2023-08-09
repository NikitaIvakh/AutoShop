using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Order;

namespace AutoShop.Service.Interfaces
{
    public interface IBasketService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItemsAsync(string userName);

        Task<IBaseResponse<OrderViewModel>> GetItemAsync(long id, string userName);
    }
}