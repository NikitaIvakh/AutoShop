using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.User;

namespace AutoShop.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync();

        Task<IBaseResponse<UserViewModel>> GetUserAsync(long id);

        Task<IBaseResponse<User>> DeleteAsync(long id);
    }
}