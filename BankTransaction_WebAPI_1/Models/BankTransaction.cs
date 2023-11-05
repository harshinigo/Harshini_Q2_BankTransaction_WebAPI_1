namespace BankTransaction_WebAPI_1.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime Date { get; set; }
        public string? Narration { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
