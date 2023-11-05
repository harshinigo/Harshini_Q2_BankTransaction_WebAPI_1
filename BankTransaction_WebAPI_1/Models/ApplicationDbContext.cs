using Microsoft.EntityFrameworkCore;

namespace BankTransaction_WebAPI_1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RawBankTransaction> RawBankTransaction { get; set; }
        public DbSet<BankTransaction> BankTransaction { get; set; }
    }
}
