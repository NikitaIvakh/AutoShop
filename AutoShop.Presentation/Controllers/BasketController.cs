using AutoShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Detail()
        {
            var response = await _basketService.GetItemsAsync(User.Identity?.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data.ToList());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(long id)
        {
            var response = await _basketService.GetItemAsync(id, User.Identity?.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return PartialView(response.Data);

            return RedirectToAction("Index", "Home");
        }
    }
}