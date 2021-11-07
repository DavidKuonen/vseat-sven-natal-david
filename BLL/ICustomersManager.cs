using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICustomersManager
    {
        Customer AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomers();
    }
}