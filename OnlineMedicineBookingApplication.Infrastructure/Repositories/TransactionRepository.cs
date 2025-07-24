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

        public TransactionRepository(MedicineAppContext context)
        {
            _context = context;


        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                             .FirstOrDefaultAsync(o => o.OrderId == transaction.OrderId);

            if (order == null) throw new ArgumentException("Order not found");

            if (order.PaymentStatus != "Completed")
            {
                order.PaymentStatus = "Completed";
                order.OrderStatus = "Confirmed";
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }


        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
