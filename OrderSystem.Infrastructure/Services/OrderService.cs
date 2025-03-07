﻿using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces;
using OrderSystem.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSystem.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
