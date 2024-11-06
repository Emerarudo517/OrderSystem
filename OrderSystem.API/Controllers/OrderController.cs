using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace OrderSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderID }, createdOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}
