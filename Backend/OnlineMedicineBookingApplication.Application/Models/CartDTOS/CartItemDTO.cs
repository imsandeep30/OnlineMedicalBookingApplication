using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.CartDTOS
{
    public class CartItemDTO
    {
        public string MedicneName { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
