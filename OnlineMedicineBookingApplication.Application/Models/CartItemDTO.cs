using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class CartItemDTO
    {
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
