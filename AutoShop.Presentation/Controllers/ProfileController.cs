using AutoShop.Domain.ViewModels.Profile;
using AutoShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail()
        {
            var userName = User.Identity?.Name;
            var response = await _profileService.GetProfileAsync(userName);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel profileViewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserName");
            ModelState.Remove("NewPassword");
            if (ModelState.IsValid)
            {
                var response = await _profileService.UpdateAsync(profileViewModel);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    return Json(new { description = response.Description });
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}