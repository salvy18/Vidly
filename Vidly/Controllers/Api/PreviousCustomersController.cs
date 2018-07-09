using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class PreviousCustomersController : ApiController
    {

        private MyContext _context;
        public PreviousCustomersController()
        {
            _context = new MyContext();
        }

        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }


        // GET /api/customers/1  (signle customer)

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        //POST /api/customers  (creating a customer)
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;

        }

        //PUT /api/customer/1 (modifying a customer)
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customerInDB.Name = customer.Name;
            customerInDB.MembershipTypeId = customer.MembershipTypeId;
            customerInDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDB.Birthday = customer.Birthday;

            _context.SaveChanges();

        }

        // DELETE /api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
        }


    }
}
