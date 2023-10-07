using Microsoft.EntityFrameworkCore;
using ProductTrackApp.Data.Contexts;
using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Data.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ProductTrackAppDbContext _context;

        public EFProductRepository(ProductTrackAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Product>> GetAllProductAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<bool> IsProductExistsAsync(int productId)
        {
            return await _context.Products.AnyAsync(x => x.Id == productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
