﻿using AutoShop.Domain.ViewModels.Car;
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
                return View(response.Data);

            return View("GetCars");
        }

        [HttpGet]
        public async Task<ActionResult> GetCar(int id, bool isJson)
        {
            var response = await _carService.GetCarAsync(id);
            if (isJson)
                return Json(response.Data);

            return PartialView("GetCar", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetCar(string term)
        {
            var response = await _carService.GetCarAsync(term);
            return Json(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _carService.GetCarAsync(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return PartialView(response.Data);

            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarViewModel carViewModel)
        {
            ModelState.Remove("Id");
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

        public IActionResult Compare()
        {
            return PartialView();
        }
    }
}