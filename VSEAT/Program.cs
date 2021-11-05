using DTO;
using BLL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;

namespace VSEAT
{
    class Program
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            //var memberDb = new MemberDB(Configuration);
            CustomersManager customerManager = new CustomersManager(Configuration);

            EmployeesManager employeesManager = new EmployeesManager(Configuration);



            //List of all customers
            //Console.WriteLine("List of all customers");

            /*List<Customer> customers = customerManager.GetAllCustomers();

            foreach (Customer c in customers)
            {
                Console.WriteLine(c.ToString());
            }*/

            //List of all customers where the courrier should deliver
            Console.WriteLine("List of all customers where the courrier should deliver");

            List<Customer> orderCustomers = employeesManager.GetOrdersCustomers(1);

            foreach (var oC in orderCustomers)
            {
                Console.WriteLine(oC);
            }
        }
    }
}
