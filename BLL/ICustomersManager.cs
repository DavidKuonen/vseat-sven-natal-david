using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICustomersManager
    {
        Customer AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int idCustomer);
        Customer GetCustomers(string Email, string Password);
        void UpdateCustomer(Customer customer);
    }
}