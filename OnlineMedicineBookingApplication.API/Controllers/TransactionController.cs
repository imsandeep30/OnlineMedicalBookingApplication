using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Application.Services;
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
        public async Task<IActionResult> AddTransaction(TransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                return BadRequest("Transaction data cannot be null.");
            }
            await _transactionService.AddTransactionAsync(transactionDto);
            return Ok("Transaction added successfully.");
        }
        [HttpGet("AllTransactions")]
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
