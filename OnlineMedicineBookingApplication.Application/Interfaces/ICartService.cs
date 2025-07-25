using OnlineMedicineBookingApplication.Application.Models.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(int userId);
        Task AddOrUpdateItemAsync(int userId, int medicineId, int quantity);
        Task<CartDTO> RemoveItemAsync(int userId, int medicineId);
        Task ClearCartAsync(int userId);
        Task<bool> PlaceOrderFromCartAsync(int userId, string shippingAddress);
    }
}
