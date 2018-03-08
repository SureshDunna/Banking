using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }

        //Foreignkey constraints for the account
        [Required]
        public long AccountNumber { get; set; }
        public Account Account { get; set; }
    }
}