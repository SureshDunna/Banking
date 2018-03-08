using Banking.Context;
using Banking.Controllers;
using Banking.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace Banking.UnitTests
{
    public class CustomerControllerTests
    {
        private readonly Mock<CustomerContext> _context;

        private readonly CustomerController _controller;
        public CustomerControllerTests()
        {
            _context = new Mock<CustomerContext>();
            _controller = new CustomerController(_context.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Fact]
        public async Task can_return_customers()
        {
            var fakeCustomers = new Customer[]
            {
                new Customer() { FirstName = "George", PostCode = 01523 },
                new Customer() { FirstName = "Susan", PostCode = 12345 }
            };
            _context.Setup(x => x.Customers).ReturnsDbSet(fakeCustomers);

            var response = _controller.GetCustomers();

            var httpResponseMessage = await response.ExecuteAsync(new System.Threading.CancellationToken());

            var contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            var customersResponse = JsonConvert.DeserializeObject<List<Customer>>(contentResponse);

            Assert.Equal(2, customersResponse.Count);
            Assert.Contains(customersResponse, x => x.FirstName == "George");
            Assert.Contains(customersResponse, x => x.FirstName == "Susan");
        }

        [Fact]
        public async Task returns_not_found_if_customer_does_not_exist()
        {
            var fakeCustomers = new Customer[]
            {
                new Customer() {CustomerId = 1,  FirstName = "George", PostCode = 01523 },
                new Customer() {CustomerId = 2,  FirstName = "Susan", PostCode = 12345 }
            };
            _context.Setup(x => x.Customers).ReturnsDbSet(fakeCustomers);

            var response = await _controller.GetCustomerById(3);

            var httpResponseMessage = await response.ExecuteAsync(new System.Threading.CancellationToken());

            Assert.Equal(HttpStatusCode.NotFound, httpResponseMessage.StatusCode);
        }
    }
}
