using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IEmployeesManager
    {
        Employee AddEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        Employee GetEmployee(string email, string password);
        Employee GetEmployeeByDistrict(int idDistrict);
        Employee GetEmployeeByDistrictAndIsFree(int idDistrict);
        List<OrderMetricForEmployee> GetOrdersCustomers(int idEmployee);
    }
}