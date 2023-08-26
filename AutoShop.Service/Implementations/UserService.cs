using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Extensions;
using AutoShop.Domain.Helpers;
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
        private readonly IBaseRepository<Profile> _profileRepository;

        public UserService(IBaseRepository<User> userRepository, ILogger<AccountService> logger, IBaseRepository<Profile> profileRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _profileRepository = profileRepository;
        }

        public async Task<IBaseResponse<User>> CreateUserAsync(UserViewModel userViewModel)
        {
            try
            {
                var user = await _userRepository.GetAllElements().FirstOrDefaultAsync(key => key.Name == userViewModel.Name);
                if (user is not null)
                {
                    return new BaseResponse<User>
                    {
                        Description = $"A user with this login already exists",
                        StatusCode = StatusCode.UserAlreadyExists,
                    };
                }

                user = new User
                {
                    Name = userViewModel.Name,
                    Password = HashPasswordHelper.HashPassword(userViewModel.Password),
                    Role = Enum.Parse<Role>(userViewModel.Role),
                };

                await _userRepository.CreateAsync(user);

                var profile = new Profile
                {
                    Address = string.Empty,
                    Age = 0,
                    UserId = user.Id,
                };

                await _profileRepository.CreateAsync(profile);

                return new BaseResponse<User>
                {
                    Data = user,
                    Description = $"User successfully added",
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.CreateUserAsync] : {ex.Message}");
                return new BaseResponse<User>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllElements().Select(key => new UserViewModel
                {
                    Id = key.Id,
                    Name = key.Name,
                    Role = key.Role.GetDisplayName(),
                }).ToListAsync();

                if (users is null)
                {
                    return new BaseResponse<IEnumerable<UserViewModel>>()
                    {
                        Description = $"Users not found",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                _logger.LogInformation($"[UserService.GetUsersAsync] elements received - {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.GetUsersAsync] : {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Description = $"[UserService.GetUsersAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<UserViewModel>> GetUserAsync(long id)
        {
            try
            {
                var user = await _userRepository.GetAllElements().Select(key => new UserViewModel
                {
                    Id = key.Id,
                    Name = key.Name,
                    Role = key.Role.GetDisplayName(),
                }).FirstOrDefaultAsync(key => key.Id == id);

                if (user is null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = $"User not found",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                return new BaseResponse<UserViewModel>()
                {
                    Data = user,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.GetUserAsync] : {ex.Message}");
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[UserService.GetUserAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public IBaseResponse<IDictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role))).ToDictionary(key => (int)key, value => value.GetDisplayName());
                if (roles is null)
                {
                    return new BaseResponse<IDictionary<int, string>>()
                    {
                        Description = $"There are no roles",
                        StatusCode = StatusCode.NotFountRoles,
                    };
                }

                return new BaseResponse<IDictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.GetRoles] : {ex.Message}");
                return new BaseResponse<IDictionary<int, string>>()
                {
                    Description = ex.Message,
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
                _logger.LogError(ex, $"[UserService.DeleteAsync] : {ex.Message}");
                return new BaseResponse<bool>
                {
                    Description = $"[UserService.DeleteAsync] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}