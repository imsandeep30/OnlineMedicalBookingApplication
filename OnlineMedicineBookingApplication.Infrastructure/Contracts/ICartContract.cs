using OnlineMedicineBookingApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface ICartContract
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task AddOrUpdateItemAsync(int userId, int medicineId, int quantity);
        Task<Cart> RemoveItemAsync(int userId, int medicineId);
        Task ClearCartAsync(int userId);
        Task SaveChangesAsync();
        Task DefaultCart(Cart cart);
    }
}
