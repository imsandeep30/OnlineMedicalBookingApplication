using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface IOrderContract
    {
        // Add a new order
        Task<Order> AddOrderAsync(Order order);
        // Add transaction if payment status is completed
        //Task AddTransactionIfPaymentCompletedAsync(Order order);
        // View order status by user (user-specific)
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        // View specific order by ID (user/admin)
        Task<Order> GetOrderByIdAsync(int orderId);
        // View all orders (admin)
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        // Update order status (admin)
        Task UpdateOrderStatusAsync(int orderId, string newStatus);
        // Update order (validate before update)
        Task<bool> UpdateOrderAsync(Order order);
        // Cancel order
        Task<bool> CancelOrderAsync(int orderId);

        Task SaveChangesAsync();
    }
}
