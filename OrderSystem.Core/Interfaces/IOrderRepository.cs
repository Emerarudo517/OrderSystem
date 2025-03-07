﻿using OrderSystem.Core.Entities;

namespace OrderSystem.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
