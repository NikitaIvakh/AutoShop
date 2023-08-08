using AutoShop.Domain.Extensions;
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
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsersAsync();
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.GetUserAsync(id);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    return PartialView("GetUser", response.Data);
            }

            var errorModel = ModelState.Values.SelectMany(key => key.Errors.Select(key => key.ErrorMessage));
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorModel });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.DeleteAsync(id);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    return RedirectToAction("GetUsers");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Save()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.CreateUserAsync(userViewModel);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    return Json(new { description = response.Description });

                return BadRequest(new { errorMessage = response.Description });
            }

            var errorMessage = ModelState.Values.SelectMany(key => key.Errors.Select(key => key.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            var types = _userService.GetRoles();
            return Json(types.Data);
        }
    }
}