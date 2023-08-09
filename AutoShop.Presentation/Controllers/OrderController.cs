using AutoShop.Domain.ViewModels.Order;
using AutoShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult CreateOrder(long id)
        {
            var orderModel = new CreateOrderViewModel
            {
                CarId = id,
                Login = User.Identity?.Name,
                Quantity = 0,
            };

            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel createOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.CreateAsync(createOrderViewModel);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    return Json(new { description = response.Description });
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderService.DeleteAsync(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return RedirectToAction("Detail", "Basket");

            return View("Error", $"{response.Description}");
        }
    }
}