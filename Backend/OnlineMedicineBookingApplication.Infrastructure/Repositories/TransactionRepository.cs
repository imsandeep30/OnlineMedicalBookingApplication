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
            // Find the associated order and include its items
            var order = await _context.Orders.Include(o => o.OrderItems)
                                             .FirstOrDefaultAsync(o => o.OrderId == transaction.OrderId);

            if (order == null)
                throw new ArgumentException("Order not found");

            // If payment is not already marked as completed, update it
            if (order.PaymentStatus != "Completed")
            {
                order.PaymentStatus = "Completed";
                order.OrderStatus = "Confirmed";
            }

            // Add the transaction record
            _context.Transactions.Add(transaction);

            // Save both transaction and order updates
            await _context.SaveChangesAsync();

            return transaction;
        }

        // Fetches all transaction records from the database
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
