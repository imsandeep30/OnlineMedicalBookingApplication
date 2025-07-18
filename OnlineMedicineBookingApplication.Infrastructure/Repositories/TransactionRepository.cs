using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MedicineAppContext _context;

        public TransactionRepository()
        {
            _context = new MedicineAppContext(); 
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
