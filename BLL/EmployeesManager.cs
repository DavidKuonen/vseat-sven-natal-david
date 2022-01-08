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
        public int GetTheRightEmployee(int IdRestaurant, DateTime DeliveryTime)
        {
            //Get the Restaurant District Id
            int DistrictRestaurant = RestaurantsDB.GetRestaurantById(IdRestaurant).IdDistrict;

            //Goes through all employees
            for (int i = 0; i < GetAllEmployees().Count; i++)
            {
                //See if the employee is in the same district as the restaurant
                if (DistrictRestaurant == GetAllEmployees()[i].IdDistrict)
                {
                    int IdEmployee = GetAllEmployees()[i].IdEmployee;
                    int Counter = 0;

                    //Looks how many orders the Employees has in the next 30min
                    var Order = OrdersDB.GetOpenOrdersEmployee(IdEmployee);

                    //If there is no order yet then insert a new entry
                    if (Order == null)
                    {
                        UpdateOpenOrders(IdEmployee);
                        return IdEmployee;
                    }

                    foreach (Orders or in Order)
                    {
                        //Checks if the DeliveryTime is already available in the next 30 minutes
                        if (or.DeliveryTime < DeliveryTime.AddMinutes(30) && or.DeliveryTime >= DeliveryTime.AddMinutes(-30))
                        {
                            Counter++;
                        }
                    }

                    if (Counter < 5)
                    {
                        //Update the OpenOrders counter of the employee
                        UpdateOpenOrders(IdEmployee);
                        return IdEmployee;
                    }
                    Counter = 0;
                }
            }
            return 0;
        }
    }
}