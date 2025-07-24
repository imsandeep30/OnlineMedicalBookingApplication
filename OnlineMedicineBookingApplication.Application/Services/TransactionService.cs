using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.TransactionDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.Repositories;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionContract _transactionRepository;
        private readonly IOrderContract _orderRepository;
        public TransactionService(ITransactionContract repository, IOrderContract orderRepository)
        {
            _transactionRepository = repository;
            _orderRepository = orderRepository;
        }

        public async Task<TransactionResponseDto> AddTransactionAsync(TransactionDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Transaction data cannot be null.");
            }
            var order = await _orderRepository.GetOrderByIdAsync(dto.OrderId);
            var transaction = new Transaction
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                Amount = order.TotalAmount,
            };

            var result = await _transactionRepository.AddTransactionAsync(transaction);
            if (result == null)
            {
                return null; // Transaction failed, order not placed
            }
            return new TransactionResponseDto
            {
                TransactionId = result.TransactionId,
                OrderId = result.OrderId,
                PaymentMethod = result.PaymentMethod,
                Amount = result.Amount,
                TransactionDate = result.TransactionDate
            };
        }

        public async Task<List<TransactionResponseDto>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
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
