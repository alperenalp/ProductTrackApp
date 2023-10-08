using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;

namespace ProductTrackApp.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductAsync();
            return View(products);
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
