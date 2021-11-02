using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ICustomersDB
    {
        Customer AddCustomer(Customer customer);
        
        List<Customer> GetAllCustomers();

        Customer GetCustomerByDistrict(int idDistrict);

        Customer GetCustomer(string email, string password);
    }
}