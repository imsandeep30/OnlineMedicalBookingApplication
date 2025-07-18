using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetAllTransactionsAsync();
    }
}
