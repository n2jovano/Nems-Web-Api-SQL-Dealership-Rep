using DealershipApp.Data;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using System.Diagnostics.Metrics;

namespace DealershipApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;         
        }

        public bool CustomerExists(int customerId)
        {
            return _context.Customers.Any(p => p.Id == customerId);
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
        }

        public ICollection<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.Id).ToList();
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);

            return Save();
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);

            return Save();
        }

        public bool Save()
        {
            var stateSaved = _context.SaveChanges();  //sql generated and sent to db

            return stateSaved > 0 ? true : false;
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return Save();
        }
    }
}
