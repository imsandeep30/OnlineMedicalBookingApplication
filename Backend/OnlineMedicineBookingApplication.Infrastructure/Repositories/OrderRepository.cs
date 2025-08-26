using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class OrderRepository : IOrderContract
    {
        private readonly MedicineAppContext _context;

        // Constructor to inject the database context
        public OrderRepository(MedicineAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Adds a new order to the database
        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            var res = await _context.Orders
                .OrderByDescending(o=>o.OrderId)
                .Include(o => o.OrderItems) // load related order items
                .FirstOrDefaultAsync(o => o.UserId == order.UserId && o.OrderStatus=="Pending");
            if (res != null)
            {
                return res; // Return the newly added order
            }
            else
            {
                throw new InvalidOperationException("Failed to add order. Please try again.");
            }
        }

        // Retrieves all orders for a specific user
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)              // Load order items
                .Include(o => o.User)                    // Include User entity
                    .ThenInclude(u => u.Address)        // Then include the Address of the User
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }


        // Gets a specific order by its ID, including related user data
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)  // Include order items
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        // Retrieves all orders, including user data, ordered by recent first
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)   // <-- Include Order Items collection
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
        }

        // Updates the status of a specific order
        public async Task UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = newStatus;
                _context.Orders.Update(order);
            }
        }

        // Updates an existing order's details if not already delivered or canceled
        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.OrderId);
            if (existingOrder == null)
                return false;

            if (existingOrder.OrderStatus == "Delivered" || existingOrder.OrderStatus == "Canceled")
                return false; // Disallow update for final status orders

            // Update editable fields
            existingOrder.ShippingAddress = order.ShippingAddress;
            existingOrder.PaymentStatus = order.PaymentStatus;
            existingOrder.TotalAmount = order.TotalAmount;

            _context.Orders.Update(existingOrder);
            return true;
        }

        // Cancels an order if it hasn't been delivered yet
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null && order.OrderStatus != "Delivered")
            {
                order.OrderStatus = "Canceled";
                if (order.PaymentStatus == "Completed")
                {
                    order.PaymentStatus = "Refund Processing";
                }
                _context.Orders.Update(order);
                return true;
            }
            return false;
        }

        // Persists any pending changes to the database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
