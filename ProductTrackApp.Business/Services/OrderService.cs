using AutoMapper;
using ProductTrackApp.Business.DTOs.Requests;
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
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateOrderAsync(AddNewOrderRequest request)
        {
            var order = _mapper.Map<Order>(request);
            return await _repository.CreateOrderAsync(order);
        }
    }
}
