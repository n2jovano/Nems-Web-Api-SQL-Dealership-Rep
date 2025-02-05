using DealershipApp.Models;

namespace DealershipApp.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int customerId);
        ICollection<Customer> GetCustomers();
        bool CustomerExists(int customerId);
        bool CreateCustomer(Customer customer);
        bool Save();
    }
}
