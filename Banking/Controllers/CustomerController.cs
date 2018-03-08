using Banking.Context;
using Banking.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Banking.Controllers
{
    /// <summary>
    /// Controller provides actions on customer
    /// </summary>
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        private readonly CustomerContext _customerContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerContext"></param>
        public CustomerController(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        /// <summary>
        /// List out all the customers 
        /// In reality we want to list out only the customers we want.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            //Prepare data to be returned using Linq as follows  
            var result = from customer in _customerContext.Customers
                            select new
                            {
                                customer.CustomerId,
                                customer.FirstName,
                                customer.LastName,
                                customer.AddressLine1,
                                customer.AddressLine2,
                                customer.PostCode,
                                customer.EmailAddress,
                                customer.MobileNumber
                            };
            return Ok(result);

        }

        /// <summary>
        /// List the customer details by customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [Route("{customerId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerById(long customerId)
        {
            var customer = await _customerContext.Customers.FindAsync(customerId);

            if(customer == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                customer.FirstName,
                customer.LastName,
                customer.AddressLine1,
                customer.AddressLine2,
                customer.PostCode,
                customer.EmailAddress,
                customer.MobileNumber
            });
        }

        /// <summary>
        /// Adds the customer to the bank
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _customerContext.Customers.Add(customer);

            await _customerContext.SaveChangesAsync();

            return StatusCode(HttpStatusCode.Created);
        }
    }
}
