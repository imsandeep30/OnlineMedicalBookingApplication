using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.TransactionDTOS;
using OnlineMedicineBookingApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
namespace OnlineMedicineBookingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("AddTransaction")]
        [Authorize(Roles = "User")] // Only users with User or Admin role can access this endpoint
        public async Task<IActionResult> AddTransaction(TransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                return BadRequest("Transaction data cannot be null.");
            }

            var result = await _transactionService.AddTransactionAsync(transactionDto);

            if (result == null)
                return BadRequest("Transaction failed. Order not placed.");

            return Ok(result); // returns transaction + order info
        }
        [HttpGet("AllTransactions")]
        [Authorize(Roles = "Admin")] // Only users with Admin role can access this endpoint
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            if(transactions == null || transactions.Count == 0)
            {
                return NotFound("No transactions found.");
            }
            return Ok(transactions);
        }
    }
}
