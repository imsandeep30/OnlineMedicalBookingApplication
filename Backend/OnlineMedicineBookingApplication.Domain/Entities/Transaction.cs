using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class transactionWork 
    {
        public string transactionwork = "//add transaction\r\n//view all transcation";
    }
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
