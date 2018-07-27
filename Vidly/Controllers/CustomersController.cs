using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private MyContext _context;

        public CustomersController()
        {
            _context = new MyContext();
        }
        // Dispouse the temp context

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index ()
        {
            //One way to retrieve Customers

            //The line below was removed because we are now retriving the data via an WEb API in the index therefore we do not need a model
            //var customers = _context.Customers.Include( c => c.MembershipType).ToList();

            //Another way to retrieve customers
            //var customers = GetCustomers();

            //var viewModel = new CustomerListViewModel();
            //viewModel.CustomerList = customers;
            //return View(customers);

            return View();
        }

        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();
            var customer = new Customer();
            var viewModel = new CustomerFormViewModel
            {
               MembershipTypes = membershipType,
               Customer = customer
               
            };

                      
            return View("CustomerForm", viewModel);
          
        }


        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save( Customer customer)
        {
            //Lets check if model is valid

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()

                };

                //Return to the same view
                return View("CustomerForm", viewModel);

            }


            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
               
            }
            else
            {
                var customerIdinDB = _context.Customers.Single(c => c.Id == customer.Id);

                customerIdinDB.Name = customer.Name;
                customerIdinDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerIdinDB.Birthday = customer.Birthday;
                customerIdinDB.MembershipTypeId = customer.MembershipTypeId;
            }


            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }



        public ActionResult Details ( int id)
        {
            //One way to do it
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            //Another way to do it
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }


        private IEnumerable<Customer> GetCustomers ()
        {
            var ctx = new MyContext();
            IEnumerable<Customer> customer = ctx.Customers.OrderBy(x=> x.Name);

            //var q = from c in ctx.Customers
            //        orderby c.Name
            //        select c;


            return customer.ToArray();
        }
    }
}