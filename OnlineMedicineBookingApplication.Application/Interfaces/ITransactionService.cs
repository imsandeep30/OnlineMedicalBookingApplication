using OnlineMedicineBookingApplication.Application.Models.TransactionDTOS;

namespace OnlineMedicineBookingApplication.Application.Services
{
    // Interface for managing transactions (e.g., payments) in the system
    public interface ITransactionService
    {
        // Adds a new transaction and creates the associated order if payment is successful
        Task<TransactionResponseDto> AddTransactionAsync(TransactionDto dto);

        // Retrieves all transactions (typically for admin reporting/monitoring)
        Task<List<TransactionResponseDto>> GetAllTransactionsAsync();
    }
}
