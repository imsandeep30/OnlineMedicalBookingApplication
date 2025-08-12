using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    // Controller to handle cart-related operations
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        // Injecting the cart service
        public readonly ICartService _cartService;

        // Constructor to initialize cart service
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get the cart for a specific user by user ID
        // Only accessible to users with the 'User' role
        [HttpGet("{id}")]
        [Authorize(Roles = "User")] // Only users with User role can access this endpoint
        public async Task<IActionResult> GetCartAsync(int id)
        {
            var cart = await _cartService.GetCartAsync(id);

            // If cart doesn't exist, return 404
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            // Return cart details
            return Ok(cart);
        }

        // Add or update an item in the cart
        // Only accessible to users with the 'User' role
        [HttpPost("add-or-update-item/{userId}/{medicineId}/{quantity}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrUpdateItemAsync(int userId, int medicineId, int quantity)
        {
            await _cartService.AddOrUpdateItemAsync(userId, medicineId, quantity);

            // Return success message
            return Ok("Item added/updated successfully.");
        }

        // Remove a specific item from the user's cart
        // Only accessible to users with the 'User' role
        [HttpDelete("remove-item/{userId}/{medicineId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveItemAsync(int userId, int medicineId)
        {
            var updatedCart = await _cartService.RemoveItemAsync(userId, medicineId);
            // If cart is null after removal, return 404
            if (updatedCart == null)
            {
                return NotFound("Item not found in cart.");
            }
            // Return updated cart details
            return Ok(updatedCart);
        }

        // Clear all items from the user's cart
        // Only accessible to users with the 'User' role
        [HttpDelete("clear-cart/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ClearCartAsync(int userId)
        {
            await _cartService.ClearCartAsync(userId);

            // Return success message
            return Ok("Cart cleared successfully.");
        }
    }
}
