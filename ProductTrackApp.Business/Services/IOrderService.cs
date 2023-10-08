using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(AddNewOrderRequest request);
        Task<IEnumerable<OrderDisplayResponse>> GetAllOrdersByManagerIdAsync(int managerId);
    }
}
