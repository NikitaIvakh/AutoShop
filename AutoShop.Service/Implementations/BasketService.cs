using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Order;
using AutoShop.Service.Interfaces;

namespace AutoShop.Service.Implementations
{
    public class BasketService : IBasketService
    {
        public Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItemsAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<OrderViewModel>> GetItemAsync(long id, string userName)
        {
            throw new NotImplementedException();
        }
    }
}