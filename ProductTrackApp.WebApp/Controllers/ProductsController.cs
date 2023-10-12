using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.DTOs.Responses;
using ProductTrackApp.Business.Services;
using ProductTrackApp.Entities;
using ProductTrackApp.WebApp.Helpers;
using ProductTrackApp.WebApp.Models;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;

namespace ProductTrackApp.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private string baseUrl = "http://localhost:7139/api";

        public ProductsController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAllProducts()
        {
            var url = baseUrl + "/products/GetAllProducts";
            var client = new Client<ProductDisplayVM>();
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var response = client.ExecuteList(url, Method.Get, accessToken, "");

            return View(response);
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateNewProductRequest request)
        {
            if (ModelState.IsValid)
            {
                request.EmployeeId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
                var url = baseUrl + "/products/AddProduct";
                var accessToken = HttpContext.Session.GetString("JwtToken");
                var client = new Client<CreateNewProductRequest>();
                var response = client.Execute(url, Method.Post, accessToken, JsonConvert.SerializeObject(request));
                return RedirectToAction(nameof(GetAllProducts));
            }
            return View();
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int id)
        {
            var url = baseUrl + $"/products/GetProductForUpdate/{id}";
            var client = new Client<UpdateProductRequest>();
            var getProductForUpdate = client.Execute(url, Method.Get, "", "");

            return View(getProductForUpdate);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateProductRequest request)
        {
            if (ModelState.IsValid)
            {
                request.EmployeeId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
                var url = baseUrl + $"/products/Edit/{id}";
                var accessToken = HttpContext.Session.GetString("JwtToken");
                var client = new Client<UpdateProductRequest>();
                var response = client.Execute(url, Method.Put, accessToken, JsonConvert.SerializeObject(request));

                return RedirectToAction(nameof(GetAllProducts));
            }
            return View();
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var url = baseUrl + $"/products/Delete/{id}";
            var accessToken = HttpContext.Session.GetString("JwtToken");
            var client = new Client<ProductDisplayVM>();
            var response = client.Execute(url, Method.Delete, accessToken, "");

            return RedirectToAction(nameof(GetAllProducts));
        }

        //[Authorize(Roles = "Employee")]
        //[HttpPost]
        //public async Task<IActionResult> ChangeStatus(int id)
        //{
        //    var isProductExists = await _productService.IsProductExistsAsync(id);
        //    if (isProductExists)
        //    {
        //        await _productService.ChangeProductStatusAsync(id);
        //        return RedirectToAction(nameof(GetAllProducts));
        //    }
        //    return NotFound();
        //}
    }
}
