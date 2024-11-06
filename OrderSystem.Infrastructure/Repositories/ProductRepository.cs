using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces;
using OrderSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSystem.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderDbContext _context;

        public ProductRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
