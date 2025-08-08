using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.OrderDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
//using OnlineMedicineBookingApplication.Application.Models;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartContract _cartRepository;
        private readonly IOrderContract _orderRepository;
        public OrderService(IOrderContract orderRepository, ICartContract cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        // Add a new order and return its ID
        public async Task<OrderResponseDTO> AddOrderAsync(OrderUserRequestDTO dto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(dto.UserId);
            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cart is empty or does not exist.");
            }

            var order = new Order
            {
                UserId = dto.UserId,
                ShippingAddress = dto.ShippingAddress,
                TotalAmount = cart.TotalPrice,
                PaymentStatus = "Pending",
                OrderStatus = "Pending",
                OrderItems = cart.Items.Select(item => new OrderItem
                {
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
            };

            var orderResult = await _orderRepository.AddOrderAsync(order);
            if (orderResult.PaymentStatus == "Completed")
            {
                //Clear the cart after placing the order
                await _cartRepository.ClearCartAsync(dto.UserId);
            }
            return new OrderResponseDTO
            {
                OrderId = orderResult.OrderId,
                UserId = orderResult.UserId,
                OrderDate = orderResult.OrderDate,
                ShippingAddress = orderResult.ShippingAddress,
                PaymentStatus = orderResult.PaymentStatus,
                OrderStatus = orderResult.OrderStatus,
                TotalAmount = orderResult.TotalAmount
            };
        }

        // Get all orders placed by a specific user
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
        // Get all orders by the admin
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
        // Get a specific order by its ID
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
        // Update the status of an existing order
        public async Task<bool> UpdateOrderStatusAsyc(OrderStatusUpdateDTO statusDto)
        {
            await _orderRepository.UpdateOrderStatusAsync(statusDto.OrderId, statusDto.NewStatus);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
        // Update the details of an existing order by a specific user
        public async Task<bool> UpdateOrderAsync(OrderUpdateDTO updateDto)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(updateDto.OrderId);
            if (existingOrder == null) return false;
            existingOrder.ShippingAddress = updateDto.ShippingAddress;
            existingOrder.PaymentStatus = updateDto.PaymentStatus;
            existingOrder.TotalAmount = updateDto.TotalAmount;
            var updated = await _orderRepository.UpdateOrderAsync(existingOrder);
            if (updated)
            {
                await _orderRepository.SaveChangesAsync();
            }
            return updated;
        }
        // Cancel an order (delete it from the system)
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
