using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class OrderUpdateDTO
    {
        public int OrderId { get; set; }
        public string? ShippingAddress { get; set; } 
        public string? PaymentStatus { get; set; } 
        public string? OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
