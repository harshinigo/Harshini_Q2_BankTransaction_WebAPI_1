using BankTransaction_WebAPI_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction_WebAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public BankTransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankTransaction>>> GetBankTransaction() //Displays all data BankTransaction
        {
            if (_dbContext.BankTransaction == null)
            {
                return NotFound();
            }
            else
            {
                return await _dbContext.BankTransaction.ToListAsync();
            }
        }


        [HttpPost("calculateAndCopyData")]
        public async Task<IActionResult> CalculateAndCopyData()
        {
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC CalculateAndCopyData");
                return Ok("Data calculation and copying complete.");
            }
            catch (Exception ex)
            {
                // Handle any errors or exceptions
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
