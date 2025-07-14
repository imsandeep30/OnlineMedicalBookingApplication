using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class Cart
    {
        [Key]
        int CartId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Dictionary<int, int> MedicineWithQuantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
    public class cartworkneed
    {
        public string[] Cartwork = { "//add to cart//remove from cart//update cart//view cart" };
    }
}
