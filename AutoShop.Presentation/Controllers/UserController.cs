using AutoShop.Domain.Enum;
using AutoShop.Domain.ViewModels.User;
using AutoShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await _userService.GetUserAsync(id);
            return PartialView("GetUser", response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var response = await _userService.GetUsersAsync();
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _userService.DeleteAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel userViewModel)
        {
            var user = Enum.Parse<Role>(userViewModel.Role);
            if (ModelState.IsValid)
                return RedirectToAction("GetUser");

            return Ok(user);
        }
    }
}