using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;
using ProductTrackApp.WebAPI.Models;
using System.Data;
using System.Security.Claims;

namespace ProductTrackApp.WebAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserService _userService;

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
            List<OrderDisplayVM> ordersVM = await getAllOrdersWithVMAsync();
            return Ok(ordersVM);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("confirm/{id:int}")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null)
            {
                await SendEmailToEmployeeAsync(id);

                await SendEmailToUserAsync(id);

                await _orderService.DeleteOrderByIdAsync(id);

                return Ok();
            }
            return NotFound($"Order with id {id} is not found.");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("reject/{id:int}")]
        public async Task<IActionResult> RejectOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null)
            {
                await _productService.ShowProductAsync(order.ProductId);

                await _orderService.DeleteOrderByIdAsync(id);

                return Ok();
            }
            return NotFound($"Order with id {id} is not found.");
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(int productId)
        {
            bool productIsExists = await _productService.IsProductExistsAsync(productId);
            if (productIsExists)
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
                
                AddNewOrderRequest request = new AddNewOrderRequest
                {
                    productId = productId,
                    userId = userId
                };

                int orderId = await _orderService.CreateOrderAsync(request);
                if (orderId == 0)
                {
                    return BadRequest();
                }
                else
                {
                    await _productService.HideProductAsync(productId);
                }

                await SendEmailToManagerAsync(orderId);

                return Ok($"Order with id {orderId} is created.");
            }
            else
            {
                return NotFound($"Product with id {productId} is not found.");
            }
        }

        private async Task<List<ProductDisplayVM>> getAllProductsWithVMAsync()
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

        private async Task<List<OrderDisplayVM>> getAllOrdersWithVMAsync()
        {
            var managerId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value);
            var orders = await _orderService.GetAllOrdersByManagerIdAsync(managerId);
            var ordersVM = new List<OrderDisplayVM>();
            foreach (var order in orders)
            {
                var user = await _userService.GetUserByIdAsync(order.UserId);
                var product = await _productService.GetProductByIdAsync(order.ProductId);
                var vm = new OrderDisplayVM
                {
                    Id = order.Id,
                    Product = product.Brand + " " + product.Model,
                    UserName = user.Name + " " + user.LastName,
                    UserEmail = user.Email
                };
                ordersVM.Add(vm);
            }

            return ordersVM;
        }

        private async Task SendEmailToManagerAsync(int orderId)
        {
            string from = "alperen-a@outlook.com";
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var user = await _userService.GetUserByIdAsync(order.UserId);
            var product = await _productService.GetProductByIdAsync(order.ProductId);
            string htmlContent = $@"<html>
                                     <body>
                                       <p>{user.Name} {user.LastName} adlı kullanıcı ürün talebinde bulundu, talep edilen ürün bilgileri aşağıdaki gibidir. </p> 
                                       <p>Ürün Marka: {product.Brand} </p> 
                                       <p>Model: {product.Model} </p>
                                       <p>Kategori: {product.Category} </p>
                                       <p>Ürün Kodu: {product.ProductCode} </p>
                                     </body>
                                    </html>";
            var userManager = await _userService.GetUserByIdAsync((int)user.ManagerId);
            string to = userManager.Email;
            await _emailSenderService.SendEmailAsync(htmlContent, from, to);
        }

        private async Task SendEmailToEmployeeAsync(int orderId)
        {
            string from = "alperen-a@outlook.com";
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var user = await _userService.GetUserByIdAsync(order.UserId);
            var product = await _productService.GetProductByIdAsync(order.ProductId);
            string htmlContent = $@"<html>
                                     <body>
                                       <p>{user.Name} {user.LastName} adlı kişinin ürün talebi yöneticisi tarafından onaylanmıştır, lütfen ilgili ürünü kullanıcıya teslim ediniz. Teslim edilmesi gereken ürün bilgileri aşağıdaki gibidir. </p> 
                                       <p>Ürün Marka: {product.Brand} </p> 
                                       <p>Model: {product.Model} </p>
                                       <p>Kategori: {product.Category} </p>
                                       <p>Ürün Kodu: {product.ProductCode} </p>
                                     </body>
                                  </html>";
            var employee = await _userService.GetUserByIdAsync((int)product.EmployeeId);
            await _emailSenderService.SendEmailAsync(htmlContent, from, employee.Email);
        }

        private async Task SendEmailToUserAsync(int orderId)
        {
            string from = "alperen-a@outlook.com";
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var user = await _userService.GetUserByIdAsync(order.UserId);
            var product = await _productService.GetProductByIdAsync(order.ProductId);
            var userManager = await _userService.GetUserByIdAsync((int)user.ManagerId);
            string htmlContent = $@"<html>
                                     <body>
                                       <p>Yöneticiniz {userManager.Name} {userManager.LastName}, ürün talebinizi onayladı, ürününüzü depo biriminden teslim alabilirsiniz. Ürün bilgileri aşağıdaki gibidir. </p> 
                                       <p>Ürün Marka: {product.Brand} </p> 
                                       <p>Model: {product.Model} </p>
                                       <p>Kategori: {product.Category} </p>
                                       <p>Ürün Kodu: {product.ProductCode} </p>
                                     </body>
                                  </html>";
            await _emailSenderService.SendEmailAsync(htmlContent, from, user.Email);
        }

    }
}
