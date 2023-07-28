using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Presentation.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response1 = await _carRepository.SelectAsync();
            var responde2 = await _carRepository.GetByNameAsync("Porsche 911");
            var responde3 = await _carRepository.GetAsync(3);

            var car = new Car()
            {
                Name = "Tesla Model X",
                Model = "Tesla",
                Description = " luxury all-electric crossover SUV produced by Tesla",
                Speed = 250,
                Price = 295470,
                DateCreate = DateTime.UtcNow,
                TypeCar = Domain.Enum.TypeCar.Suv
            };

            var responde4 = await _carRepository.CreateAsync(car);
            var responde5 = await _carRepository.DeleteAsync(car);

            return View(response1);
        }
    }
}