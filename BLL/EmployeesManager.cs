using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EmployeesManager : IEmployeesManager
    {
        private IEmployeesDB EmployeesDB { get; }
        private IOrdersDB OrdersDB { get; }
        private IRestaurantsDB RestaurantsDB { get; }

        public EmployeesManager(IEmployeesDB EmployeesDB, IOrdersDB OrdersDB, IRestaurantsDB RestaurantsDB)
        {
            this.EmployeesDB = EmployeesDB;
            this.OrdersDB = OrdersDB;
            this.RestaurantsDB = RestaurantsDB;
        }

        //SQL queries
        public Employee AddEmployee(Employee employee)
        {
            return EmployeesDB.AddEmployee(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return EmployeesDB.GetAllEmployees();
        }

        public Employee GetEmployee(string email, string password)
        {
            return EmployeesDB.GetEmployee(email, password);
        }

        public void UpdateOpenOrders(int EmployeeId)
        {
            EmployeesDB.UpdateOpenOrders(EmployeeId);
        }


        //SQL logic across multiple databases and DALs
        public int GetTheRightEmployee(int idRestaurant, DateTime DeliveryTime)
        {
            //Get the Restaurant District Id
            int districtRestaurant = RestaurantsDB.GetRestaurantById(idRestaurant).IdDistrict;

            //Goes through all employees
            for (int i = 0; i < GetAllEmployees().Count; i++)
            {
                //See if the employee is in the same district as the restaurant
                if (districtRestaurant == GetAllEmployees()[i].IdDistrict)
                {
                    int id = GetAllEmployees()[i].IdEmployee;

                    //Looks how many orders the Employees has in the next 30min
                    int number = OrdersDB.GetOrdersNotDelivered(id, DeliveryTime);

                    //If it's less than with give back the id of the Employee
                    if (number < 5)
                    {
                        //Update the OpenOrders counter of the employee
                        UpdateOpenOrders(id);
                        return id;
                    }
                }
            }
            return 0;
        }
    }
}
