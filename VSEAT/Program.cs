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

            //Exercise List of all members
            Console.WriteLine("Exercise List of all members");

            List<Customer> customers = customerManager.GetAllCustomers();

            foreach (Customer c in customers)
            {
                Console.WriteLine(c.ToString());
            }
        }
    }
}
