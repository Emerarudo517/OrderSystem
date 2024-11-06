using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces;
using OrderSystem.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSystem.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            // Business logic, for example, validating product details before adding
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Business logic, for example, ensuring the product exists before updating
            var existingProduct = await _productRepository.GetProductByIdAsync(product.ProductID);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            // Business logic, for example, checking if the product is associated with any orders
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            await _productRepository.DeleteProductAsync(id);
        }
    }
}
