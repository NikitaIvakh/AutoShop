using AutoShop.Service.Interfaces;
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
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCarsAsync();
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }
    }
}