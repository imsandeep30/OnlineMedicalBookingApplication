using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface ITransactionContract
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetAllTransactionsAsync();
    }
}
