using BankTransaction_WebAPI_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction_WebAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawBankTransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public RawBankTransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawBankTransaction>>> GetRawBankTransaction() //Displays all data of RawBankTransaction
        {
            if (_dbContext.RawBankTransaction == null)
            {
                return NotFound();
            }
            return await _dbContext.RawBankTransaction.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<RawBankTransaction>> AddRawBankTransaction(RawBankTransaction rawBankTransaction) //Add new transaction
        {
            _dbContext.RawBankTransaction.Add(rawBankTransaction);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRawBankTransaction), new { id = rawBankTransaction.Id }, rawBankTransaction);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRawBankTransaction(int Id, RawBankTransaction rawBankTransaction)
        {
            if (Id != rawBankTransaction.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(rawBankTransaction).State = EntityState.Modified;   //Updatting database
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RawBankTransactionExists(Id))    //Id/data passed from the frontend 
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        private bool RawBankTransactionExists(int Id)  //Check if the transaction with Id exit in the database or not
        {
            return (_dbContext.RawBankTransaction?.Any(x => x.Id == Id)).GetValueOrDefault();
        }

        [HttpGet("ID")]
        public async Task<ActionResult<RawBankTransaction>> GetRawBankTransactionID(int ID) //Gives single transaction for given ID
        {
            if (_dbContext.RawBankTransaction == null)
            {
                return NotFound();
            }
            var RawBankTransaction = await _dbContext.RawBankTransaction.FindAsync(ID);
            if (RawBankTransaction == null)
            {
                return NotFound();
            }
            return RawBankTransaction;
        }
        
        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteRawBankTransaction(int ID)
        {
            if (_dbContext.RawBankTransaction == null)
            {
                return NotFound();
            }
            var RawBankTransaction = await _dbContext.RawBankTransaction.FindAsync(ID);
            if (RawBankTransaction == null)
            {
                return NotFound();
            }
            _dbContext.RawBankTransaction.Remove(RawBankTransaction);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

