using AutoShop.Domain.ViewModels.Car;
using AutoShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Presentation.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            var response = _carService.GetCars();
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data.ToList());

            return View("Error", $"{response.Description}");
        }

        public async Task<ActionResult> GetCar(int id)
        {
            var response = await _carService.GetCarAsync(id);
            return PartialView("GetCar", response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View();

            var response = await _carService.GetCarAsync(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            ModelState.AddModelError("", response.Description);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarViewModel carViewModel)
        {
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (carViewModel.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(carViewModel.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)carViewModel.Avatar.Length);
                    }

                    await _carService.CreateCarAsync(carViewModel, imageData);
                }

                else
                    await _carService.EditCarAsync(carViewModel);

                return RedirectToAction("GetCars");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCarAsync(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return RedirectToAction("GetCars");

            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _carService.GetTypes();
            return Json(types.Data);
        }
    }
}