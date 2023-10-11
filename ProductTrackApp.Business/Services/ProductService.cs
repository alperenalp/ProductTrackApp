using AutoMapper;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.DTOs.Responses;
using ProductTrackApp.Data.Repositories;
using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task ChangeProductStatusAsync(int productId)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            product.Status = !product.Status;
            await _repository.UpdateProductAsync(product);
        }

        public async Task CreateProductAsync(CreateNewProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            await _repository.CreateProductAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _repository.DeleteProductAsync(productId);
        }

        public async Task<IEnumerable<ProductDisplayResponse>> GetAllProductAsync()
        {
            var products = await _repository.GetAllProductAsync();
            return _mapper.Map<IEnumerable<ProductDisplayResponse>>(products);
        }

        public async Task<IEnumerable<ProductDisplayResponse>> GetNotHiddenProductsAsync()
        {
            var products = await _repository.GetNotHiddenProductsAsync();
            return _mapper.Map<IEnumerable<ProductDisplayResponse>>(products);
        }

        public async Task<ProductDisplayResponse> GetProductByIdAsync(int productId)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDisplayResponse>(product);
        }

        public async Task<UpdateProductRequest> GetProductForUpdateAsync(int productId)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            return _mapper.Map<UpdateProductRequest>(product);
        }

        public async Task HideProductAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            product.Status = false;
            await _repository.UpdateProductAsync(product);
        }

        public async Task<bool> IsProductExistsAsync(int productId)
        {
            return await _repository.IsProductExistsAsync(productId);
        }

        public async Task ShowProductAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            product.Status = true;
            await _repository.UpdateProductAsync(product);
        }

        public async Task UpdateProductAsync(UpdateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            product.Status = true;
            await _repository.UpdateProductAsync(product);
        }
    }
}
