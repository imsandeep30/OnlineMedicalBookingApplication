using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class OrderRequestDTO
    {
        public int UserId { get; set; }
       public string ShippingAddress { get; set; }
        public string PaymentStatus { get; set; } 
        public decimal TotalAmount { get; set; } 
    }
}
