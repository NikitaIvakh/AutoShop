using AutoShop.Domain.Entity;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Profile;

namespace AutoShop.Service.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<ProfileViewModel>> GetProfileAsync(string userName);

        Task<IBaseResponse<Profile>> SaveAsync(ProfileViewModel profileViewModel);
    }
}