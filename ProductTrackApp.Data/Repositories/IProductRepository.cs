using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Data.Repositories
{
    public interface IProductRepository
    {
        Task<int> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int ProductId);
        Task<Product?> GetProductByIdAsync(int ProductId);
        Task<IList<Product>> GetAllProductAsync();
        Task<bool> IsProductExistsAsync(int ProductId);
        Task<IList<Product>> GetNotHiddenProductsAsync();
    }
}
