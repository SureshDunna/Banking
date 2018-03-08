using Banking.Models;
using System.Data.Entity;

namespace Banking.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("CustomerContext") { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}