using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private MyContext _context;
        public CustomersController()
        {
            _context = new MyContext();
        }

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }


        // GET /api/customers/1  (signle customer)

        public CustomerDto GetCustomer ( int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Customer,CustomerDto>(customer);
        }

        //POST /api/customers  (creating a customer)
        [HttpPost]
        public CustomerDto CreateCustomer ( CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return customerDto;

        }

        //PUT /api/customer/1 (modifying a customer)
        [HttpPut]
        public void UpdateCustomer (int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null )
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(customerDto, customerInDB);

           _context.SaveChanges();

        }

        // DELETE /api/customer/1
        [HttpDelete]
        public void DeleteCustomer (int id)
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
