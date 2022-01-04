using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CustomersManager : ICustomersManager
    {
        private ICustomersDB CustomersDB { get; }

        public CustomersManager(ICustomersDB CustomersDB)
        {
            this.CustomersDB = CustomersDB;
        }

        //SQL Befehle der DAL Klasse
        public Customer GetCustomers(string Email, string Password)
        {
            return CustomersDB.GetCustomer(Email, Password);
        }

        public Customer GetCustomerById(int idCustomer)
        {
            return CustomersDB.GetCustomerById(idCustomer);
        }

        public List<Customer> GetAllCustomers()
        {
            return CustomersDB.GetAllCustomers();
        }

        public Customer AddCustomer(Customer customer)
        {
            return CustomersDB.AddCustomer(customer);
        }
        //SQL Befehle bis hier
    }
}
