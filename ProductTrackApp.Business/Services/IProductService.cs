using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateNewProductRequest request);
        Task DeleteProductAsync(int productId);
        Task<ProductDisplayResponse> GetProductByIdAsync(int productId);
        Task<UpdateProductRequest> GetProductForUpdateAsync(int productId);
        Task<IEnumerable<ProductDisplayResponse>> GetAllProductAsync();
        Task<bool> IsProductExistsAsync(int productId);
        Task UpdateProductAsync(UpdateProductRequest request);
    }
}
