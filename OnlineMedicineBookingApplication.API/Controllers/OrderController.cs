using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.OrderDTOS;
using Microsoft.AspNetCore.Authorization;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //orderController
    public class OrderController : ControllerBase
    {
        //Injecting the Order Service Interface
        private readonly IOrderService _orderService;
        //dependency Injection
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("place-order")]
        [Authorize(Roles = "User")] // Only users with User or Admin role can access this endpoint
        public async Task<IActionResult> CreateOrder([FromBody] OrderUserRequestDTO orderDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderId = await _orderService.AddOrderAsync(orderDto);
            return Ok(orderDto);
        }
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "User,Admin")] // Only users with User or Admin role can access this endpoint
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this user.");
            }
            return Ok(orders);
        }
        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found.");
            }
            return Ok(orders);
        }
        [HttpGet("GetByOrderId")]
        [Authorize(Roles = "Admin")] // Only users with User or Admin role can access this endpoint
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }
        [HttpPut("status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateDTO statusUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _orderService.UpdateOrderStatusAsyc(statusUpdateDto);
            if (!isUpdated)
            {
                return NotFound("Order not found.");
            }
            return Ok("Order status updated successfully.");
        }
        [HttpPut("OrderUpdate")]
        [Authorize(Roles = "User,Admin")] // Both User and Admin can update orders
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=await _orderService.UpdateOrderAsync(updateDto);
            return result ? Ok(result) : NotFound("Order not found");
        }
        [HttpDelete("{orderId}")]
        [Authorize(Roles = "User,Admin")] // Both User and Admin can cancel orders
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var result=await _orderService.CancelOrderAsync(orderId);
            return result ? Ok("Order Cancelled ") : NotFound("Order not found or cannot be cancelled.");
        }

    }
}
