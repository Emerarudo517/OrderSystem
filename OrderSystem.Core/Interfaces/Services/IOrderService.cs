using OrderSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSystem.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
