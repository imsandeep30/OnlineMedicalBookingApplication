using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class CartRepository : ICartContract
    {
        private readonly MedicineAppContext _context;
        private readonly UserRepository _userRepository;

        public CartRepository(MedicineAppContext context)
        {
            _context = context;
        }
        public async Task DefaultCart(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException($"No user found with ID {userId}", nameof(userId));
            }
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
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
                    CartId = cart.CartId, 
                    MedicineId = medicineId,
                    Quantity = quantity,
                    Price = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId)?.Price ?? 0
                });
            }

            cart.TotalPrice = cart.Items.Sum(i =>
                i.Quantity * (_context.Medicines.FirstOrDefault(m => m.MedicineId == i.MedicineId)?.Price ?? 0));

            await _context.SaveChangesAsync();
        }

        public async Task<Cart> RemoveItemAsync(int userId, int medicineId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            var itemToRemove = cart.Items.FirstOrDefault(i => i.MedicineId == medicineId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                cart.TotalPrice = cart.Items.Sum(i =>
                    i.Quantity * (_context.Medicines.FirstOrDefault(m => m.MedicineId == i.MedicineId)?.Price ?? 0));

                await _context.SaveChangesAsync();
            }
            return cart;
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
