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

        // Constructor injecting repositories needed to manage transactions and orders
        public TransactionService(ITransactionContract repository, IOrderContract orderRepository)
        {
            _transactionRepository = repository;
            _orderRepository = orderRepository;
        }

        // Add a new transaction for an order
        public async Task<TransactionResponseDto> AddTransactionAsync(TransactionDto dto)
        {
            // Validate input DTO
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Transaction data cannot be null.");
            }

            // Check if the order exists
            var order = await _orderRepository.GetOrderByIdAsync(dto.OrderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found", nameof(dto.OrderId));
            }

            // Prevent duplicate payments
            else if (order.PaymentStatus == "Completed")
            {
                throw new InvalidOperationException("Order payment already completed.");
            }

            // Create a new transaction object using the order's total amount
            var transaction = new Transaction
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                Amount = order.TotalAmount,
            };

            // Save the transaction
            var result = await _transactionRepository.AddTransactionAsync(transaction);

            // Return null if transaction failed
            if (result == null)
            {
                return null;
            }

            // Return transaction details in DTO format
            return new TransactionResponseDto
            {
                TransactionId = result.TransactionId,
                OrderId = result.OrderId,
                PaymentMethod = result.PaymentMethod,
                Amount = result.Amount,
                TransactionDate = result.TransactionDate
            };
        }

        // Get all transactions recorded in the system
        public async Task<List<TransactionResponseDto>> GetAllTransactionsAsync()
        {
            // Retrieve all transactions from the repository
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            // Return null if no transactions exist
            if (transactions.Count == 0)
            {
                return null;
            }

            // Convert transaction entities to DTOs
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
