using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IEmployeesManager
    {
        Employee AddEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        Employee GetEmployee(string email, string password);
        Employee GetEmployeeByDistrict(int idDistrict);
        int GetTheRightEmployee(int idRestaurant, DateTime DeliveryTime);
        Employee GetEmployeeByDistrictAndIsFree(int idDistrict);
        void UpdateOpenOrders(int EmployeeId);
    }
}