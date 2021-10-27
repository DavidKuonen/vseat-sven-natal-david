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

        public EmployeesManager(IConfiguration conf)
        {
            EmployeesDB = new EmployeesDB(conf);
        }

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

    }
}
