using OnlineMedicineBookingApplication.Application.Models;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(TransactionDto dto);
        Task<List<TransactionResponseDto>> GetAllTransactionsAsync();
    }
}
