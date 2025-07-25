using OnlineMedicineBookingApplication.Application.Models.TransactionDTOS;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> AddTransactionAsync(TransactionDto dto);
        Task<List<TransactionResponseDto>> GetAllTransactionsAsync();
    }
}
