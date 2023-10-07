using Microsoft.AspNetCore.Mvc;

namespace ProductTrackApp.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult GetAllProducts()
        {
            return View();
        }
    }
}
