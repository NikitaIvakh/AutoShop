﻿using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Profile;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoShop.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<Profile> _baseRepository;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(IBaseRepository<Profile> baseRepository, ILogger<ProfileService> logger)
        {
            _baseRepository = baseRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<ProfileViewModel>> GetProfileAsync(string userName)
        {
            try
            {
                var profile = await _baseRepository.GetAllElements().Include(key => key.User).Select(key => new ProfileViewModel
                {
                    Id = key.Id,
                    Age = key.Age,
                    Address = key.Address,
                    UserName = key.User.Name,
                }).FirstOrDefaultAsync(key => key.UserName == userName);

                if (profile is null)
                {
                    return new BaseResponse<ProfileViewModel>()
                    {
                        Description = $"Profile is null",
                    };
                }

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ProfileService.GetAsync] : {ex.Message}");
                return new BaseResponse<ProfileViewModel>
                {
                    Description = $"[ProfileService.GetAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Profile>> SaveAsync(ProfileViewModel profileViewModel)
        {
            try
            {
                var profile = await _baseRepository.GetAllElements().FirstOrDefaultAsync(key => key.Id == profileViewModel.Id);
                if (profile is null)
                {
                    return new BaseResponse<Profile>()
                    {
                        Description = $"Profile is null",
                    };
                }

                profile.Age = profileViewModel.Age;
                profile.Address = profileViewModel.Address;

                await _baseRepository.UpdateAsync(profile);

                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ProfileService.SaveAsync] : {ex.Message}");
                return new BaseResponse<Profile>
                {
                    Description = $"[ProfileService.SaveAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}