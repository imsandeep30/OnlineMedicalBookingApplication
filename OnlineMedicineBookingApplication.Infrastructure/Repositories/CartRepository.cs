using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly MedicineAppContext _context;

        public CartRepository(MedicineAppContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task AddOrUpdateItemAsync(int userId, int medicineId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(i => i.MedicineId == medicineId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    MedicineId = medicineId,
                    Quantity = quantity
                });
            }

            cart.TotalPrice = cart.Items.Sum(i =>i.Quantity * _context.Medicines.FirstOrDefault(m => m.MedicineId == i.MedicineId)?.Price ?? 0);

            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            cart.Items.Clear();
            cart.TotalPrice = 0;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
