using OrderSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSystem.Core.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int id);
    }
}
