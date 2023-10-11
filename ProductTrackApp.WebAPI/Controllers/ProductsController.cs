using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;
using System.Data;
using System.Security.Claims;

namespace ProductTrackApp.WebAPI.Models
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductsController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productsVM = await GetAllProductsWithVMAsync();
            return Ok(productsVM);
        }

        //[Authorize(Roles = "Employee")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProduct(CreateNewProductRequest request)
        {
            if (ModelState.IsValid)
            {
                //request.EmployeeId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
                await _productService.CreateProductAsync(request);
                return StatusCode(201, request);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, UpdateProductRequest request)
        {
            var isProductExists = await _productService.IsProductExistsAsync(id);
            if (isProductExists)
            {
                if (request.Id == id)
                {
                    if (ModelState.IsValid)
                    {
                        request.EmployeeId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
                        await _productService.UpdateProductAsync(request);
                        return Ok(request);
                    }
                    return BadRequest(ModelState);
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"Product with id:{id} could not match request with id:{request.Id}"
                });
            }
            return NotFound();
        }

        [Authorize(Roles = "Employee")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id)
        {
            var isProductExists = await _productService.IsProductExistsAsync(id);
            if (isProductExists)
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            return NotFound(new
            {
                StatusCode = 404,
                Message = $"Product with id:{id} could not found."
            });
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
    }
}
