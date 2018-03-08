using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public int PostCode { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string MobileNumber { get; set; }

        public List<Account> Accounts { get; set; }

        public Customer()
        {
            Accounts = new List<Account>();
        }
    }
}