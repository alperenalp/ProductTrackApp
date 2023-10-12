using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.DTOs.Responses;
using ProductTrackApp.Business.Services;
using ProductTrackApp.Entities;
using ProductTrackApp.WebApp.Helpers;
using ProductTrackApp.WebApp.Models;
using System.Security.Claims;
using RestSharp;
using Newtonsoft.Json;

namespace ProductTrackApp.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserService _userService;
        private string baseUrl = "http://localhost:7139/api";

        public OrdersController(IProductService productService, IOrderService orderService, IEmailSenderService emailSenderService, IUserService userService)
        {
            _productService = productService;
            _orderService = orderService;
            _emailSenderService = emailSenderService;
            _userService = userService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
            var url = baseUrl + $"/orders/GetAllOrders/{userId}";
            var client = new Client<OrderDisplayVM>();
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var response = client.ExecuteList(url, Method.Get, accessToken, "");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var url = baseUrl + $"/orders/ConfirmOrder/{id}";
            var client = new Client<OrderDisplayResponse>();
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var response = client.Execute(url, Method.Get, accessToken, "");

            return RedirectToAction(nameof(GetAllOrders));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> RejectOrder(int id)
        {
            var url = baseUrl + $"/orders/RejectOrder/{id}";
            var client = new Client<OrderDisplayResponse>();
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var response = client.Execute(url, Method.Get, accessToken, "");

            return RedirectToAction(nameof(GetAllOrders));
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrder()
        {
            var url = baseUrl + $"/products/GetAllProducts";
            var client = new Client<ProductDisplayVM>();
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var response = client.ExecuteList(url, Method.Get, accessToken, "");

            return View(response);
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

            var url = baseUrl + $"/orders/AddOrder/{id}";
            var client = new Client<AddNewOrderRequest>();
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var response = client.Execute(url, Method.Post, accessToken, JsonConvert.SerializeObject(request));

            return RedirectToAction(nameof(AddOrder));

        }

    }
}
