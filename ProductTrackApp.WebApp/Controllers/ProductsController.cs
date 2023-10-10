using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;
using ProductTrackApp.Entities;
using ProductTrackApp.WebApp.Models;
using System.Security.Claims;

namespace ProductTrackApp.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductsController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productsVM = await GetAllProductsWithVMAsync();
            return View(productsVM);
        }

        private async Task<List<ProductDisplayVM>> GetAllProductsWithVMAsync()
        {
            var products = await _productService.GetNotHiddenProductsAsync();
            var productsVM = new List<ProductDisplayVM>();
            foreach (var product in products)
            {
                var user = await _userService.GetUserByIdAsync((int)product.EmployeeId);
                var vm = new ProductDisplayVM
                {
                    ProductCode = product.ProductCode,
                    Brand = product.Brand,
                    Category = product.Category,
                    Id = product.Id,
                    Model = product.Model,
                    Status = product.Status,
                    EmployeeName = user.Name,
                };
                productsVM.Add(vm);
            }

            return productsVM;
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
                await _productService.CreateProductAsync(request);
                return RedirectToAction(nameof(GetAllProducts));
            }
            return View();
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _productService.GetProductForUpdateAsync(id);
            return View(products);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateProductRequest request)
        {
            var isProductExists = await _productService.IsProductExistsAsync(id);
            if (isProductExists)
            {
                if (ModelState.IsValid)
                {
                    await _productService.UpdateProductAsync(request);
                    return RedirectToAction(nameof(GetAllProducts));
                }
                return View();
            }
            return NotFound();
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var isProductExists = await _productService.IsProductExistsAsync(id);
            if (isProductExists)
            {
                await _productService.DeleteProductAsync(id);
                return RedirectToAction(nameof(GetAllProducts));
            }
            return NotFound();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var isProductExists = await _productService.IsProductExistsAsync(id);
            if (isProductExists)
            {
                await _productService.ChangeProductStatusAsync(id);
                return RedirectToAction(nameof(GetAllProducts));
            }
            return NotFound();
        }
    }
}
