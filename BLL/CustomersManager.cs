using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CustomersManager
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
        //SQL Befehle bis hier
    }
}
