using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ICustomersDB
    {
        Customer AddCustomer(Customer customer);
        
        List<Customer> GetAllCustomers();

        Customer GetCustomerByVillage(int idVillage);

        Customer GetCustomer(string email, string password);
    }
}