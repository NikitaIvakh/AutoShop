using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Helpers;
using AutoShop.Domain.Response;
using AutoShop.Domain.ViewModels.Account;
using AutoShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AutoShop.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<User> baseRepository, ILogger<AccountService> logger)
        {
            _userRepository = baseRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel register)
        {
            try
            {
                var user = await _userRepository.GetAllElements().FirstOrDefaultAsync(key => key.Name == register.Name);
                if (user is not null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = $"A user with this username already exists",
                    };
                }

                user = new User
                {
                    Name = register.Name,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassword(register.Password)
                };

                await _userRepository.CreateAsync(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = $"The object was added",
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Register]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel login)
        {
            try
            {
                var user = await _userRepository.GetAllElements().FirstOrDefaultAsync(key => key.Name == login.Name);
                if (user is null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User not found"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(login.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = $"Invalid password",
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = $"All good",
                    StatusCode = StatusCode.Ok,
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Login]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        private static ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}