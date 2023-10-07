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
    public class EFOrderRepository : IOrderRepository
    {
        private readonly ProductTrackAppDbContext _context;

        public EFOrderRepository(ProductTrackAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Order>> GetAllOrderAsync()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.AsNoTracking().SingleOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<bool> IsOrderExistsAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(x => x.Id == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
