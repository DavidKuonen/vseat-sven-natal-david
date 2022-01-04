using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EmployeesManager : IEmployeesManager
    {
        private IEmployeesDB EmployeesDB { get; }
        private IOrdersDB OrdersDB { get; }
        private ICustomersDB CustomersDB { get; }
        private IOrder_DishesDB Order_DishesDB { get; }
        private IDishesDB DishesDB { get; }
        private IRestaurantsDB RestaurantsDB { get; }
        private IVillagesDB VillagesDB { get; }

        public EmployeesManager(IEmployeesDB EmployeesDB, IOrdersDB OrdersDB, IRestaurantsDB RestaurantsDB)
        {
            this.EmployeesDB = EmployeesDB;
            this.OrdersDB = OrdersDB;
            this.RestaurantsDB = RestaurantsDB;
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

        public Employee GetEmployeeByDistrictAndIsFree(int idDistrict)
        {
            return EmployeesDB.GetEmployeeByDistrictAndIsFree(idDistrict);
        }

        public void UpdateOpenOrders(int EmployeeId)
        {
            EmployeesDB.UpdateOpenOrders(EmployeeId);
        }


        //Logik über mehrere DB
        public int GetTheRightEmployee(int idRestaurant, DateTime DeliveryTime)
        {
            int districtRestaurant = RestaurantsDB.GetRestaurantById(idRestaurant).idDistrict;

            for (int i = 0; i < GetAllEmployees().Count; i++)
            {
                if (districtRestaurant == GetAllEmployees()[i].IdDistrict)
                {
                    int id = GetAllEmployees()[i].IdEmployee;
                    int number = OrdersDB.GetOrdersNotDelivered(id, DeliveryTime);
                    if (number < 5)
                    {
                        UpdateOpenOrders(id);
                        return id;
                    }
                }
            }
            return 0;
        }
    }
}
