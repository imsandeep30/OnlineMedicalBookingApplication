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
        public OrderRepository()
        {
            _context = new MedicineAppContext();
        }
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

        }
        //public async Task AddTransactionIfPaymentCompletedAsync(Order order)
        //{
        //    // Logic to add transaction if payment status is completed
        //    if (order.PaymentStatus == "Completed")
        //    {
        //        // Add transaction logic here

        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.User).OrderByDescending(o => o.OrderDate).ToListAsync(); ;
        }
        public async Task UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = newStatus;
                _context.Orders.Update(order);
            }
        }
        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.OrderId);
            if (existingOrder == null)
            {
                return false;
            }
            if (existingOrder.OrderStatus == "Delivered" || existingOrder.OrderStatus == "Canceled")
            {
                return false; // Cannot update delivered or canceled orders
            }
            if (existingOrder != null)
            {
                existingOrder.ShippingAddress = order.ShippingAddress;
                existingOrder.PaymentStatus = order.PaymentStatus;
                existingOrder.TotalAmount = order.TotalAmount;
                _context.Orders.Update(existingOrder);
                return true;
            }
            return false;
        }
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null && order.OrderStatus != "Delivered")
            {
                order.OrderStatus = "Canceled";
                _context.Orders.Update(order);
                return true;
            }
            return false;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }
    }
}
