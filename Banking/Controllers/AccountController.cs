using Banking.Context;
using Banking.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Banking.Controllers
{
    /// <summary>
    /// Provides the actions to perform on accounts
    /// </summary>
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController
    {
        private CustomerContext _customerContext = new CustomerContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerContext"></param>
        public AccountController(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        /// <summary>
        /// List out the accounts information based on the customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [Route("customer/{customerId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountsByCustomerId(long customerId)
        {
            var customer = await _customerContext.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var accounts = await _customerContext.Database.SqlQuery<AccountDTO>
                ($"SELECT a.AccountNumber, a.AccountName, (sum(t.Credit) - sum(t.Debit)) as AccountBalance FROM dbo.Accounts a LEFT OUTER JOIN dbo.Transactions t ON a.AccountNumber = t.AccountNumber WHERE a.CustomerId = {customerId} " +
                $"group by a.AccountNumber, a.AccountName").ToListAsync();

            return Ok(accounts);
        }

        /// <summary>
        /// List out the account information by account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [Route("{accountNumber}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountByAccountNumber(long accountNumber)
        {
            var account = await _customerContext.Accounts.FindAsync(accountNumber);

            if (account == null)
            {
                return NotFound();
            }

            var accountResponse = await _customerContext.Database.SqlQuery<AccountDTO>
                ($"SELECT a.AccountNumber, a.AccountName, (sum(t.Credit) - sum(t.Debit)) as AccountBalance FROM dbo.Accounts a LEFT OUTER JOIN dbo.Transactions t ON a.AccountNumber = t.AccountNumber WHERE a.AccountNumber = {accountNumber} " +
                $"group by a.AccountNumber, a.AccountName").ToListAsync();

            return Ok(accountResponse);
        }

        /// <summary>
        /// Adds the new account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody, Required] Account account)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = await _customerContext.Customers.FindAsync(account.CustomerId);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Accounts.Add(account);

            await _customerContext.SaveChangesAsync();

            return StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// It gets transaction history for the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [Route("transactions/{accountNumber}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTransactionHistory(long accountNumber)
        {
            var transactions = await _customerContext.Transactions.SqlQuery
                ($"SELECT * FROM dbo.Transactions WHERE Accountnumber = {accountNumber}").ToListAsync();

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        /// <summary>
        /// Adds the transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [Route("transactions/add")]
        [HttpPost]
        public async Task<IHttpActionResult> AddTransaction([FromBody, Required] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var account = await _customerContext.Accounts.FindAsync(transaction.AccountNumber);

            if (account == null)
            {
                return NotFound();
            }

            account.Transactions.Add(transaction);

            await _customerContext.SaveChangesAsync();

            return Ok();
        }
    }
}
