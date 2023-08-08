using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.User;

namespace AutoShop.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> CreateUserAsync(UserViewModel userViewModel);

        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync();

        Task<IBaseResponse<UserViewModel>> GetUserAsync(long id);

        IBaseResponse<IDictionary<int, string>> GetRoles();

        Task<IBaseResponse<bool>> DeleteAsync(long id);
    }
}