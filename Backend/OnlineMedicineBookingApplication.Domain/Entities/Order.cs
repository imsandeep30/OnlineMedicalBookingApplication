using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class  OrderWork
    {
        public string orderwork = "//add transaction call if payment status is completed\r\n//view order status this is for user to view his order status\r\n//view all orders this is for admin to view all orders\r\n//add order \r\n// update order status\r\n// update order validation\r\n//cancel order";
    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; } 

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderStatus { get; set; } = "Pending";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        
        public Transaction Transaction { get; set; }

    }
}
