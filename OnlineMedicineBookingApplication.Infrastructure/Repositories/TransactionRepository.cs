using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using OnlineMedicineBookingApplication.Infrastructure.Repositories;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MedicineAppContext _context;

        public TransactionRepository(MedicineAppContext context)
        {
<<<<<<< HEAD
            _context = context;
=======
            _context = context; 
>>>>>>> e69256bc966d438776dd72df19349d22c2f8f64f
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
