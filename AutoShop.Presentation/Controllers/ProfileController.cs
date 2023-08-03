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
        public async Task<IActionResult> ProfileInfo()
        {
            var userName = User.Identity?.Name;
            var response = await _profileService.GetAsync(userName);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            return View();
        }

        [HttpGet]
        public IActionResult Save()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                if (profileViewModel.Id == 0)
                    await _profileService.CreateAsync(profileViewModel);

                else
                    await _profileService.EditAsync(profileViewModel);

                return RedirectToAction("ProfileInfo");
            }

            return View();
        }
    }
}