using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Profile;

namespace AutoShop.Service.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<Profile>> CreateAsync(ProfileViewModel profileViewModel);

        Task<IBaseResponse<ProfileViewModel>> GetAsync(string userName);

        Task<IBaseResponse<Profile>> EditAsync(ProfileViewModel profileViewModel);
    }
}