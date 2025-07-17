using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrderAsync(OrderRequestDTO orderDto);
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<OrderResponseDTO>> GetAllOrdersAsync();
        Task<OrderResponseDTO> GetOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderStatusAsyc(OrderStatusUpdateDTO statusDto);
        Task<bool> UpdateOrderAsync(OrderUpdateDTO updateDto);
        Task<bool> CancelOrderAsync(int orderId);
    }
}
