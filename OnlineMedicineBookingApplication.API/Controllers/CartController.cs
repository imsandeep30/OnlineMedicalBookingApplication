using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartAsync(int id)
        {
            var cart = await _cartService.GetCartAsync(id);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }
            return Ok(cart);
        }
        [HttpPost("add-or-update-item")]
        public async Task<IActionResult> AddOrUpdateItemAsync(int userId, int medicineId, int quantity)
        {
            await _cartService.AddOrUpdateItemAsync(userId, medicineId, quantity);
            return Ok("Item added/updated successfully.");
        }
        [HttpDelete("clear-cart/{userId}")]
        public async Task<IActionResult> ClearCartAsync(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok("Cart cleared successfully.");
        }
    }
}
