using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Profile;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<Profile> _baseRepository;

        public ProfileService(IBaseRepository<Profile> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IBaseResponse<Profile>> CreateAsync(ProfileViewModel profileViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<Profile>> GetAsync(string userName)
        {
            try
            {
                var car = await _baseRepository.GetAllElements().Include(key => key.User).FirstOrDefaultAsync(key => key.User.Name == userName);
                if (car is null)
                {
                    return new BaseResponse<Profile>()
                    {
                        Description = $"User not found",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                return new BaseResponse<Profile>()
                {
                    Data = car,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<Profile>
                {
                    Description = $"[Get] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public Task<IBaseResponse<Car>> EditAsync(ProfileViewModel profileViewModel)
        {
            throw new NotImplementedException();
        }
    }
}