using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.OrderDTOS;
using Microsoft.AspNetCore.Authorization;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    // OrderController handles all order-related operations
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // Injecting the Order Service Interface
        private readonly IOrderService _orderService;

        // Constructor injection for IOrderService
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Place a new order (User role only)
        [HttpPost("place-order")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderUserRequestDTO orderDto)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call service to add order
            var orderId = await _orderService.AddOrderAsync(orderDto);

            // Return placed order details
            return Ok(orderId);
        }

        // Get all orders for a specific user (User and Admin roles)
        [HttpGet("GetOrderByUserId/{userId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);

            // If no orders found
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this user.");
            }

            // Return list of orders
            return Ok(orders);
        }

        // Get all orders in the system (Admin only)
        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            // If no orders found
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found.");
            }

            // Return all orders
            return Ok(orders);
        }

        // Get a specific order by order ID (Admin only)
        [HttpGet("GetByOrderId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            // If order not found
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Return order details
            return Ok(order);
        }

        // Update order status (Admin only)
        [HttpPut("status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateDTO statusUpdateDto)
        {
            // Validate input model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call service to update order status
            var isUpdated = await _orderService.UpdateOrderStatusAsyc(statusUpdateDto);

            // Check update result
            if (!isUpdated)
            {
                return NotFound("Order not found.");
            }

            return Ok("Order status updated successfully.");
        }

        // Update order details (User and Admin roles)
        [HttpPut("OrderUpdate")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO updateDto)
        {
            // Validate input model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call service to update the order
            var result = await _orderService.UpdateOrderAsync(updateDto);

            // Return result or not found
            return result ? Ok(result) : NotFound("Order not found");
        }

        // Cancel an order by order ID (User and Admin roles)
        [HttpDelete("Cancel/{orderId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            // Call service to cancel the order
            var result = await _orderService.CancelOrderAsync(orderId);

            // Return result or not found
            return result ? Ok("Order Cancelled ") : NotFound("Order not found or cannot be cancelled.");
        }
    }
}
