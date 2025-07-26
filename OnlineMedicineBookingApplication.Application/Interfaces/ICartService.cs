using OnlineMedicineBookingApplication.Application.Models.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    // Interface for Cart-related operations
    public interface ICartService
    {
        // Retrieves the cart details for a specific user
        Task<CartDTO> GetCartAsync(int userId);

        // Adds a new item to the cart or updates its quantity if it already exists
        Task AddOrUpdateItemAsync(int userId, int medicineId, int quantity);

        // Removes a specific item (medicine) from the user's cart
        Task<CartDTO> RemoveItemAsync(int userId, int medicineId);

        // Clears all items from the user's cart
        Task ClearCartAsync(int userId);

        // Places an order from the items currently in the cart
        Task<bool> PlaceOrderFromCartAsync(int userId, string shippingAddress);
    }
}
