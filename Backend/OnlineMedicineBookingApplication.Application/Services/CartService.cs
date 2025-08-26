using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Application.Models.CartDTOS;
using OnlineMedicineBookingApplication.Application.Models.OrderDTOS;
using OnlineMedicineBookingApplication.Application.Models.TransactionDTOS;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Services
{
    // Service class handling cart-related operations
    public class CartService : ICartService
    {
        private readonly ICartContract _cartRepository;
        private readonly IOrderService _orderService;
        private readonly ITransactionService _transactionService;
        private readonly IMedicineService _medicineService;

        // Constructor injecting necessary dependencies
        public CartService(ICartContract cartRepository, IOrderService orderService, ITransactionService transactionService, IMedicineService medicineService)
        {
            _cartRepository = cartRepository;
            _orderService = orderService;
            _transactionService = transactionService;
            _medicineService = medicineService;
        }

        // Retrieves the user's cart with all items
        public async Task<CartDTO> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            return new CartDTO
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    MedicneName = i.MedicineName,
                    MedicineId = i.MedicineId,
                    Quantity = i.Quantity,
                    Price = i.Price,
                }).ToList()
            };
        }

        // Adds or updates an item in the user's cart
        public async Task AddOrUpdateItemAsync(int userId, int medicineId, int quantity)
        {
            await _cartRepository.AddOrUpdateItemAsync(userId, medicineId, quantity);
        }

        // Clears all items from the user's cart
        public async Task ClearCartAsync(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }

        // Removes a specific medicine from the user's cart
        public async Task<CartDTO> RemoveItemAsync(int userId, int medicineId)
        {
            var cart = await _cartRepository.RemoveItemAsync(userId, medicineId);
            return new CartDTO
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    MedicineId = i.MedicineId,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
