using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.TransactionDTOS;
using OnlineMedicineBookingApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    // Controller for managing transaction-related operations
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // Injecting the transaction service
        private readonly ITransactionService _transactionService;

        // Constructor injection for ITransactionService
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Endpoint to create a new transaction (User only)
        [HttpPost("AddTransaction")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddTransaction(TransactionDto transactionDto)
        {
            // Check if the incoming transaction data is null
            if (transactionDto == null)
            {
                return BadRequest("Transaction data cannot be null.");
            }

            // Attempt to add the transaction
            var result = await _transactionService.AddTransactionAsync(transactionDto);

            // If transaction fails
            if (result == null)
                return BadRequest("Transaction failed. Order not placed.");

            // Return transaction + order details
            return Ok(result);
        }

        // Endpoint to get all transactions (Admin only)
        [HttpGet("AllTransactions")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTransactions()
        {
            // Fetch all transaction records
            var transactions = await _transactionService.GetAllTransactionsAsync();

            // If no transactions are found
            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("No transactions found.");
            }

            // Return the list of transactions
            return Ok(transactions);
        }
    }
}
