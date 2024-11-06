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
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderService = new OrderService(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnOrder_WhenOrderIsValid()
        {
            // Arrange
            var newOrder = new Order { OrderID = 1, TrangThai = "Pending", TongTien = 500 };
            _orderRepositoryMock.Setup(repo => repo.AddOrderAsync(newOrder)).Returns(Task.CompletedTask);

            // Act
            var createdOrder = await _orderService.CreateOrderAsync(newOrder);

            // Assert
            Assert.NotNull(createdOrder);
            Assert.Equal(newOrder.OrderID, createdOrder.OrderID);
            _orderRepositoryMock.Verify(repo => repo.AddOrderAsync(newOrder), Times.Once);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var existingOrder = new Order { OrderID = orderId, TrangThai = "Completed" };
            _orderRepositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId)).ReturnsAsync(existingOrder);

            // Act
            var order = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(orderId, order.OrderID);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 1;
            _orderRepositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act
            var order = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldThrowException_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderToUpdate = new Order { OrderID = 1, TrangThai = "Pending" };
            _orderRepositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderToUpdate.OrderID)).ReturnsAsync((Order)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.UpdateOrderAsync(orderToUpdate));
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldCallRepositoryDelete_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var existingOrder = new Order { OrderID = orderId };
            _orderRepositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId)).ReturnsAsync(existingOrder);

            // Act
            await _orderService.DeleteOrderAsync(orderId);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.DeleteOrderAsync(orderId), Times.Once);
        }
    }
}
