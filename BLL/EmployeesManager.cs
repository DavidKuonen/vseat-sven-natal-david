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

        public EmployeesManager(IConfiguration conf)
        {
            EmployeesDB = new EmployeesDB(conf);
            OrdersDB = new OrdersDB(conf);
            CustomersDB = new CustomersDB(conf);
            Order_DishesDB = new Order_DishesDB(conf);
            DishesDB = new DishesDB(conf);
            RestaurantsDB = new RestaurantsDB(conf);
            VillagesDB = new VillagesDB(conf);
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
            return GetEmployeeByDistrictAndIsFree(idDistrict);
        }
        //SQL Befehle bis hier

        //Logik über mehrere DB
        public List<OrderMetricForEmployee> GetOrdersCustomers(int idEmployee)
        {

            var orders = OrdersDB.GetOrdersByStaffId(idEmployee);

            List<OrderMetricForEmployee> getOrder = new List<OrderMetricForEmployee>();

            List<int> idCustomers = new List<int>();
            List<int> idOrder = new List<int>();
            List<int> idDish = new List<int>();
            List<int> idRestaurant = new List<int>();
            List<int> idVillage = new List<int>();

            foreach (var o in orders)
            {

                //Console.WriteLine("FK Customers: " + o.FK_Customers);
                //Console.WriteLine("FK Orders: " + o.idOrders);
                idCustomers.Add(o.FK_Customers);
                idOrder.Add(o.idOrders);


            }

            foreach (var idC in idCustomers)
            {

                idVillage.Add(CustomersDB.GetCustomerById(idC).IdVillage);
            }


            foreach (var idO in idOrder)
            {
                Console.WriteLine("-----------------ID ORDER: " + idO);
                var orderDishes = Order_DishesDB.GetOrderDishesByOrderId(idO);

                foreach (var idOD in orderDishes)
                {
                    Console.WriteLine("-----------------ID DISH: " + idOD.FK_Dishes);
                    idDish.Add(idOD.FK_Dishes);
                }

            }

            foreach (var idD in idDish)
            {
                idRestaurant.Add(DishesDB.GetDishesById(idD).FK_Restaurant);
            }

            for (int i = 0; i < idCustomers.Count; i++)
            {
                Console.WriteLine(idDish[i]);

                var metric = new OrderMetricForEmployee
                {
                    CustomerFirstname = CustomersDB.GetCustomerById(idCustomers[i]).Firstname,
                    CustomerLastname = CustomersDB.GetCustomerById(idCustomers[i]).Lastname,
                    CustomerAddress = CustomersDB.GetCustomerById(idCustomers[i]).Address,
                    CustomerVillage = VillagesDB.GetVillageById(CustomersDB.GetCustomerById(idCustomers[i]).IdVillage).name,
                    DishName = DishesDB.GetDishesById(idDish[i]).name,
                    RestaurantName = RestaurantsDB.GetRestaurantById(idRestaurant[i]).name,
                    RestaurantVillage = VillagesDB.GetVillageById(RestaurantsDB.GetRestaurantById(idRestaurant[i]).idVillage).name

                };

                getOrder.Add(metric);
            }

            /*foreach (var idC in idCustomers)
            {
                int i = 0;
                    
                i++;  
            }*/

            return getOrder;
        }
    }
}
