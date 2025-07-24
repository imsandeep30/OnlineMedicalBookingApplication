using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models.OrderDTOS;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> AddOrderAsync(OrderUserRequestDTO orderDto);
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<OrderResponseDTO>> GetAllOrdersAsync();
        Task<OrderResponseDTO> GetOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderStatusAsyc(OrderStatusUpdateDTO statusDto);
        Task<bool> UpdateOrderAsync(OrderUpdateDTO updateDto);
        Task<bool> CancelOrderAsync(int orderId);
    }
}
