using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IList<Order>> GetAllOrderAsync();
        Task<bool> IsOrderExistsAsync(int orderId);
    }
}
