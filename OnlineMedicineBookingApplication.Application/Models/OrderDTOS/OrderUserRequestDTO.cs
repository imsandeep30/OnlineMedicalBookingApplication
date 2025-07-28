using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.OrderDTOS
{
    public class OrderUserRequestDTO
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; }
    }
}
