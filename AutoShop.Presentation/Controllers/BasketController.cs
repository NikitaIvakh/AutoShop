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
    }
}