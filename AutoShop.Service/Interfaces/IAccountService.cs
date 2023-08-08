using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Account;
using System.Security.Claims;

namespace AutoShop.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel register);

        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel login);

        Task<IBaseResponse<bool>> ChangePasswordAsync(ChangePasswordViewModel changePassword);
    }
}