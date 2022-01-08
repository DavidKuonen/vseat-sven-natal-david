using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IEmployeesDB
    {
        Employee AddEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        //Employee GetEmployeeByDistrict(int idDistrict);
        //Employee GetEmployeeById(int idEmployee);
        //Employee GetEmployeeByDistrictAndIsFree(int idDistrict);
        Employee GetEmployee(string email, string password);
        void UpdateOpenOrders(int EmployeeId);
    }
}