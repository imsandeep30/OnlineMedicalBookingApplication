using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.CartDTOS
{
    public class CartItemDTO
    {
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
