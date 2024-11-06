using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces;
using OrderSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly OrderDbContext _context;

    public OrderItemRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> GetOrderItemByIdAsync(int id)
    {
        return await _context.OrderItems.FindAsync(id);
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderID == orderId)
            .ToListAsync();
    }

    public async Task AddOrderItemAsync(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
