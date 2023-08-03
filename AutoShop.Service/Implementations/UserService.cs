using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Extensions;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.User;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoShop.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public UserService(IBaseRepository<User> userRepository, ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync()
        {
            try
            {
                var users = _userRepository.GetAllElements().AsEnumerable().Select(key => new UserViewModel
                {
                    Id = key.Id,
                    Name = key.Name,
                    Role = key.Role.GetDisplayName(),
                });

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetUsers] : {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<UserViewModel>> GetUserAsync(long id)
        {
            try
            {
                var user = _userRepository.GetAllElements().Select(key => new
                {
                    key.Id,
                    key.Name,
                    key.Profile.Age,
                    key.Profile.Address,
                    key.Role,
                }).AsEnumerable().Select(key => new UserViewModel()
                {
                    Id = key.Id,
                    Name = key.Name,
                    Age = key.Age,
                    Address = key.Address,
                    Role = key.Role.GetDisplayName(),
                }).FirstOrDefault();

                return new BaseResponse<UserViewModel>()
                {
                    Data = user,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetUser] : {ex.Message}");
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<User>> DeleteAsync(long id)
        {
            try
            {
                var user = await _userRepository.GetAllElements().FirstOrDefaultAsync(key => key.Id == id);
                if (user is null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = $"User not found",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                await _userRepository.DeleteAsync(user);

                return new BaseResponse<User>()
                {
                    Description = $"User deleted",
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<User>
                {
                    Description = $"[DeleteAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}