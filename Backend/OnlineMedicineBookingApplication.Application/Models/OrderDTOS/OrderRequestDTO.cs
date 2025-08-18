using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.OrderDTOS
{
    public class OrderRequestDTO
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } // "Completed" or "Pending"
        public List<OrderItemDTO> Items { get; set; }
    }
}
