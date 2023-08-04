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
                var users = await _userRepository.GetAllElements().Where(key => key.Role == Role.Admin).Select(key => new UserViewModel
                {
                    Id = key.Id,
                    Name = key.Name,
                    Role = key.Role.GetDisplayName(),
                }).ToListAsync();

                _logger.LogInformation($"[UserService.GetUsersAsync] elements received - {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetUsersAsync] : {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Description = $"[GetUsersAsync] : {ex.Message}",
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
                }).AsEnumerable().Select(key => new UserViewModel
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
                _logger.LogError(ex, $"[GetUserAsync] : {ex.Message}");
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[GetUserAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var user = await _userRepository.GetAllElements().FirstOrDefaultAsync(key => key.Id == id);
                if (user is null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = $"User not found",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                await _userRepository.DeleteAsync(user);

                _logger.LogInformation($"[UserService.DeleteAsync] - User deleted");
                return new BaseResponse<bool>()
                {
                    Description = $"User deleted",
                    StatusCode = StatusCode.Ok,
                    Data = true,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[DeleteAsync] : {ex.Message}");
                return new BaseResponse<bool>
                {
                    Description = $"[DeleteAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}