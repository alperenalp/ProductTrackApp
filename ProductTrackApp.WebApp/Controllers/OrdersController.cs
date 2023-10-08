using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;
using System.Security.Claims;

namespace ProductTrackApp.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrdersController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrder()
        {
            var products = await _productService.GetNotHiddenProductsAsync();
            return View(products);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(int id)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
            AddNewOrderRequest request = new AddNewOrderRequest
            {
                productId = id,
                userId = userId
            };
            int orderId = await _orderService.CreateOrderAsync(request);
            if (orderId == 0)
            {
                return BadRequest();
            }
            else
            {
                await _productService.HideProductAsync(id);
            }
            var products = await _productService.GetNotHiddenProductsAsync();
            return View(products);
        }
    }
}
