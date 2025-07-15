using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public required List<CartItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
