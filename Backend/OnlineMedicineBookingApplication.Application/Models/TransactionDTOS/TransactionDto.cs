using System;

namespace OnlineMedicineBookingApplication.Application.Models.TransactionDTOS
{
    public class TransactionDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
