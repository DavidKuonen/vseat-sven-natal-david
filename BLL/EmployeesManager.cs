using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EmployeesManager
    {
        private IEmployeesDB EmployeesDB { get; }
        private IOrdersDB OrdersDB { get; }
        private ICustomersDB CustomersDB { get; }

        public EmployeesManager(IConfiguration conf)
        {
            EmployeesDB = new EmployeesDB(conf);
            OrdersDB = new OrdersDB(conf);
            CustomersDB = new CustomersDB(conf);
        }

        //SQL Befehle der DAL Klasse
        public Employee AddEmployee(Employee employee)
        {
            return EmployeesDB.AddEmployee(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return EmployeesDB.GetAllEmployees();
        }

        public Employee GetEmployeeByDistrict(int idDistrict)
        {
            return EmployeesDB.GetEmployeeByDistrict(idDistrict);
        }

        public Employee GetEmployee(string email, string password)
        {
            return EmployeesDB.GetEmployee(email, password);
        }
        //SQL Befehle bis hier


        //Logik über mehrere DB
        public Employee GetEmployeeByDistrictAndIsFree(int idDistrict)
        {
            return GetEmployeeByDistrictAndIsFree(idDistrict);
        }

        public List<Customer> GetOrdersCustomers(int idEmployee)
        {

            var orders = OrdersDB.GetOrdersByStaffId(idEmployee);

            List<Customer> customers = new List<Customer>();
            List<int> idCustomers = new List<int>();

            foreach (var o in orders)
            {

                if (o.FK_OrderStatus == 1)
                {
                    idCustomers.Add(o.FK_Customers);
                }

            }

            foreach (var idC in idCustomers)
            {
                customers.Add(CustomersDB.GetCustomerById(idC));
            }

            return customers;
        }
        //Logik über mehrere DB bis hier
    }
}
