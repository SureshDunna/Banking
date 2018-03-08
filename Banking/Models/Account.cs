using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Account
    {
        [Key]
        public long AccountNumber { get; set; }
        [Required]
        public string AccountName { get; set; }
        public List<Transaction> Transactions { get; set; }

        //Foreignkey constraints for the customer
        [Required]
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}