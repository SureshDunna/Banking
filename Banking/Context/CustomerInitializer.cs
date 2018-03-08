using Banking.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Banking.Context
{
    public class CustomerInitializer : DropCreateDatabaseIfModelChanges<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            base.Seed(context);

            if(context.Customers.Any())
            {
                return;
            }

            var customer1 = new Customer
            {
                FirstName = "Suresh",
                LastName = "Dunna",
                AddressLine1 = "Unit 2 68 Good Street, Westmead",
                PostCode = 2145,
                MobileNumber = "0423035626",
                EmailAddress = "Sureshd84@gmail.com",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountName = "Suresh Dunna",
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                TransactionId = 1,
                                Credit = 1000,
                                AccountNumber = 12345678
                            },
                            new Transaction
                            {
                                TransactionId = 2,
                                Debit = 100,
                                AccountNumber = 12345678
                            },
                            new Transaction
                            {
                                TransactionId = 3,
                                Debit = 50,
                                AccountNumber = 12345678
                            },
                            new Transaction
                            {
                                TransactionId = 4,
                                Debit = 20,
                                AccountNumber = 12345678
                            },
                            new Transaction
                            {
                                TransactionId = 5,
                                Credit = 200,
                                AccountNumber = 12345678
                            },
                        }
                    }
                }
            };

            var customer2 = new Customer
            {
                CustomerId = 2,
                FirstName = "Chandwik",
                LastName = "Dunna",
                AddressLine1 = "Unit 2 68 Good Street, Westmead",
                PostCode = 2145,
                MobileNumber = "0423035626",
                EmailAddress = "Sureshd84@gmail.com",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountName = "Chandwik Dunna",
                        AccountNumber = 23456789,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                TransactionId = 6,
                                Credit = 3000,
                                AccountNumber = 23456789
                            },
                            new Transaction
                            {
                                TransactionId = 7,
                                Debit = 150,
                                AccountNumber = 23456789
                            },
                            new Transaction
                            {
                                TransactionId = 8,
                                Debit = 250,
                                AccountNumber = 23456789
                            },
                            new Transaction
                            {
                                TransactionId = 9,
                                Debit = 200,
                                AccountNumber = 23456789
                            },
                            new Transaction
                            {
                                TransactionId = 10,
                                Credit = 200,
                                AccountNumber = 23456789
                            },
                        }
                    }
                }
            };

            var customer3 = new Customer
            {
                CustomerId = 3,
                FirstName = "Hemalatha",
                LastName = "Erothu",
                AddressLine1 = "Unit 2 68 Good Street, Westmead",
                PostCode = 2145,
                MobileNumber = "0423035626",
                EmailAddress = "Sureshd84@gmail.com",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountName = "Hemalatha Erothu",
                        AccountNumber = 345678790,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                TransactionId = 11,
                                Credit = 2000,
                                AccountNumber = 345678790
                            },
                            new Transaction
                            {
                                TransactionId = 12,
                                Debit = 150,
                                AccountNumber = 345678790
                            },
                            new Transaction
                            {
                                TransactionId = 13,
                                Debit = 250,
                                AccountNumber = 345678790
                            },
                            new Transaction
                            {
                                TransactionId = 14,
                                Debit = 200,
                                AccountNumber = 345678790
                            },
                            new Transaction
                            {
                                TransactionId = 15,
                                Credit = 200,
                                AccountNumber = 345678790
                            },
                        }
                    }
                }
            };

            context.Customers.Add(customer1);
            context.Customers.Add(customer2);
            context.Customers.Add(customer3);
            context.SaveChanges();
        }
    }
}
