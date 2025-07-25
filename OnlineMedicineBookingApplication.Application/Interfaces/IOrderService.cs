using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models.OrderDTOS;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    // Interface for managing orders in the medicine booking application
    public interface IOrderService
    {
        // Creates a new order for a user
        Task<OrderResponseDTO> AddOrderAsync(OrderUserRequestDTO orderDto);

        // Retrieves all orders made by a specific user
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);

        // Retrieves all orders (Admin access)
        Task<IEnumerable<OrderResponseDTO>> GetAllOrdersAsync();

        // Retrieves a specific order by its unique ID
        Task<OrderResponseDTO> GetOrderByIdAsync(int orderId);

        // Updates the status of an order (e.g., Pending → Confirmed → Delivered)
        Task<bool> UpdateOrderStatusAsyc(OrderStatusUpdateDTO statusDto);

        // Updates full order details (e.g., address or quantity)
        Task<bool> UpdateOrderAsync(OrderUpdateDTO updateDto);

        // Cancels an order by ID
        Task<bool> CancelOrderAsync(int orderId);
    }
}
