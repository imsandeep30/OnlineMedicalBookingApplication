using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTransactionAsync(TransactionDto dto)
        {
            var transaction = new Transaction
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                TransactionDate = DateTime.Now
            };

            await _repository.AddTransactionAsync(transaction);
        }

        public async Task<List<TransactionResponseDto>> GetAllTransactionsAsync()
        {
            var transactions = await _repository.GetAllTransactionsAsync();
            if (transactions.Count == 0)
            {
                return null;
            }
            return transactions.Select(txn => new TransactionResponseDto
            {
                TransactionId = txn.TransactionId,
                OrderId = txn.OrderId,
                PaymentMethod = txn.PaymentMethod,
                Amount = txn.Amount,
                TransactionDate = txn.TransactionDate
            }).ToList();
        }
    }
}
