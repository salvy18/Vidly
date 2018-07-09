using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly
{
    public class GenericTest
    {

        public GenericTest()
        {
            Repository r = new Repository();
            string a = r.Add<MyClassA>(new MyClassA("aa"));

            IWebStoreContext<Customer> c = new CustonmerRepository();


            List<int> l = new List<int>();
            l.Add(10);
            l.Add(10);
            l.Add(10);
            l.Add(10);

            List<Customer> l2 = new List<Customer>();
            l2.Add(new Customer());
            l2.Add(new Customer());
            l2.Add(new Customer());
            l2.Add(new Customer());

        }

    }


    class MyClassA : IMyInterface
    {
        private string _name;

        public MyClassA(string name)
        {
            _name = name;
        }
        public string Name => _name;
    }

    class Repository
    {
        public string Add<T>(T item) where T: IMyInterface
        {
            return item.Name.ToUpper();
        }
    }

    interface IMyInterface
    {
        string Name { get; }
    }


    public class CustonmerRepository : IWebStoreContext<Customer>
    {
        private readonly MyContext _context;

        public IQueryable Items => throw new NotImplementedException();

        public CustonmerRepository()
        {
            _context = new MyContext();
        }

        public Customer Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer FindById(int ID)
        {
            return _context.Customers.Find(ID);
        }
    }

    public interface IWebStoreContext<T> where T : class
    {
        IQueryable Items { get; }
        T Add (T entity)  ;
        T FindById(int ID);
        T Delete (T entity)  ;
    }
}