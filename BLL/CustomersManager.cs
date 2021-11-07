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

        public CustomersManager(IConfiguration conf)
        {
            CustomersDB = new CustomersDB(conf);
        }

        //SQL Befehle der DAL Klasse
        public Customer GetCustomers()
        {
            return CustomersDB.GetCustomer("natal", "pwd");
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
