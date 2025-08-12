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

        // Constructor to inject the DB context
        public CartRepository(MedicineAppContext context)
        {
            _context = context;
        }

        // Adds a new cart to the database
        public async Task DefaultCart(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        // Retrieves a user's cart by user ID and includes the items in the cart
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

        // Adds a new item to the cart or updates the quantity if it already exists
        public async Task AddOrUpdateItemAsync(int userId, int medicineId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);

            // Check if item already exists in cart
            var existingItem = cart.Items.FirstOrDefault(i => i.MedicineId == medicineId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity; // Update quantity
            }
            else
            {
                // Add new item to the cart
                cart.Items.Add(new CartItem
                {
                    CartId = cart.CartId,
                    MedicineId = medicineId,
                    MedicineName = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId)?.MedicineName ?? "Unknown",
                    Quantity = quantity,
                    Price = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId)?.Price ?? 0
                });
            }

            // Update total price of the cart
            cart.TotalPrice = cart.Items.Sum(i =>
                i.Quantity * (_context.Medicines.FirstOrDefault(m => m.MedicineId == i.MedicineId)?.Price ?? 0));

            await _context.SaveChangesAsync();
        }

        // Removes a specific item from the cart and updates the total price
        public async Task<Cart> RemoveItemAsync(int userId, int medicineId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            var itemToRemove = cart.Items.FirstOrDefault(i => i.MedicineId == medicineId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);

                // Recalculate total price
                cart.TotalPrice = cart.Items.Sum(i =>
                    i.Quantity * (_context.Medicines.FirstOrDefault(m => m.MedicineId == i.MedicineId)?.Price ?? 0));

                await _context.SaveChangesAsync();
            }
            return cart;
        }

        // Clears all items in the user's cart
        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            cart.Items.Clear();
            cart.TotalPrice = 0;
            await _context.SaveChangesAsync();
        }

        // Saves changes to the database context
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
