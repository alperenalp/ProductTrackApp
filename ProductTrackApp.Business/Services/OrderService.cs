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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> CreateOrderAsync(AddNewOrderRequest request)
        {
            var order = _mapper.Map<Order>(request);
            return await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<IEnumerable<OrderDisplayResponse>> GetAllOrdersByManagerIdAsync(int managerId)
        {
            var managersUsers = await _userRepository.GetUsersByManagerIdAsync(managerId);
            var managerOrders = new List<Order>();
            var orders = await _orderRepository.GetAllOrderAsync();
            foreach (var order in orders)
            {
                if (managersUsers.Any(x => x.Id == order.UserId))
                {
                    managerOrders.Add(order);
                }
            }
            return _mapper.Map<IEnumerable<OrderDisplayResponse>>(managerOrders);
        }
    }
}
