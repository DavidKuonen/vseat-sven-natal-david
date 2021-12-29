using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ICustomersDB
    {
        Customer AddCustomer(Customer customer);
        
        List<Customer> GetAllCustomers();

        Customer GetCustomerById(int idCustomer);

        Customer GetCustomerByDistrict(int idDistrict);

        Customer GetCustomer(string Email, string Password);
    }
}