using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces;
using OrderSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .FirstOrDefaultAsync(o => o.OrderID == id)!;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
