using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using OnlineMedicineBookingApplication.Infrastructure.Repositories;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionContract
    {
        private readonly MedicineAppContext _context;

        // Constructor to inject the database context
        public TransactionRepository(MedicineAppContext context)
        {
            _context = context;
        }

        // Adds a new transaction and updates order status if payment is not completed
        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            // Find the associated order
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == transaction.OrderId);

            if (order == null)
                throw new ArgumentException("Order not found");

            // Step 1: Mark payment as completed immediately
            if (order.PaymentStatus != "Completed")
            {
                order.PaymentStatus = "Completed";
                order.OrderStatus = "Confirmed";
            }

            // Add transaction
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Step 2: Schedule order confirmation after 3 seconds (non-blocking)

            return transaction;
        }


        // Fetches all transaction records from the database
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
