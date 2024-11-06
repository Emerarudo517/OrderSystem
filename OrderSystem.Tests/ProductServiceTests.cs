using Moq;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces;
using OrderSystem.Core.Interfaces.Services;
using OrderSystem.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrderSystem.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product { ProductID = productId, TenSanPham = "Product A" };
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(existingProduct);

            // Act
            var product = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(productId, product.ProductID);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 1;
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var product = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task AddProductAsync_ShouldCallRepositoryAdd_WhenProductIsValid()
        {
            // Arrange
            var newProduct = new Product { ProductID = 1, TenSanPham = "Product A" };

            // Act
            await _productService.AddProductAsync(newProduct);

            // Assert
            _productRepositoryMock.Verify(repo => repo.AddProductAsync(newProduct), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldThrowException_WhenProductDoesNotExist()
        {
            // Arrange
            var productToUpdate = new Product { ProductID = 1, TenSanPham = "Product A" };
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productToUpdate.ProductID)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.UpdateProductAsync(productToUpdate));
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallRepositoryDelete_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product { ProductID = productId };
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(existingProduct);

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(productId), Times.Once);
        }
    }
}
