using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Models
{
    public class AccountDTO
    {
        public long AccountNumber { get; set; }
        public string AccountName { get; set; }
        public double? AccountBalance { get; set; }
    }
}