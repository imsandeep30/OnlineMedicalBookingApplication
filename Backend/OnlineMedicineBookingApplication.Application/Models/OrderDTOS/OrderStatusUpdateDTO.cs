using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.OrderDTOS
{
    public class OrderStatusUpdateDTO
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; } 
    }
}
