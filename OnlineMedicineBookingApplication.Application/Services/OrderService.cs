using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
//using OnlineMedicineBookingApplication.Application.Models;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderContract _orderRepository;
        public OrderService(IOrderContract orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<int> AddOrderAsync(OrderRequestDTO orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                ShippingAddress = orderDto.ShippingAddress,
                TotalAmount = orderDto.TotalAmount,
                PaymentStatus = orderDto.PaymentStatus
            };
            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();
            return order.OrderId;
        }
        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(o => new OrderResponseDTO
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                ShippingAddress = o.ShippingAddress,
                PaymentStatus = o.PaymentStatus,
                OrderStatus = o.OrderStatus,
                TotalAmount = o.TotalAmount
            });
        }
        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(o => new OrderResponseDTO
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                ShippingAddress = o.ShippingAddress,
                PaymentStatus = o.PaymentStatus,
                OrderStatus = o.OrderStatus,
                TotalAmount = o.TotalAmount
            });
        }
        public async Task<OrderResponseDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;
            return new OrderResponseDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.TotalAmount
            };
        }
        public async Task<bool> UpdateOrderStatusAsyc(OrderStatusUpdateDTO statusDto)
        {
            await _orderRepository.UpdateOrderStatusAsync(statusDto.OrderId, statusDto.NewStatus);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateOrderAsync(OrderUpdateDTO updateDto)
        {
            var order = new Order
            {
                OrderId = updateDto.OrderId,
                ShippingAddress = updateDto.ShippingAddress,
                PaymentStatus = updateDto.PaymentStatus,
                TotalAmount = updateDto.TotalAmount
            };
            var updated = await _orderRepository.UpdateOrderAsync(order);
            if (updated)
            {
                await _orderRepository.SaveChangesAsync();
            }
            return updated;
        }
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            var canceled = await _orderRepository.CancelOrderAsync(orderId);
            if (canceled)
            {
                await _orderRepository.SaveChangesAsync();
            }
            return canceled;

        }
    }
}
