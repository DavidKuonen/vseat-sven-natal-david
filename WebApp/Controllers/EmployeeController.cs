using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private IDishesManager DishesManager { get; }
        private IRestaurantsManager RestaurantsManager { get; }
        private IVillagesManager VillagesManager { get; }
        private IOrdersManager OrdersManager { get; }
        private ICustomersManager CustomersManager { get; }
        private IOrder_DishesManager Order_DishesManager { get; }

        public EmployeeController(IDishesManager DishesManager, IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IOrdersManager OrdersManager, ICustomersManager CustomersManager, IOrder_DishesManager Order_DishesManager)
        {
            this.DishesManager = DishesManager;
            this.RestaurantsManager = RestaurantsManager;
            this.VillagesManager = VillagesManager;
            this.OrdersManager = OrdersManager;
            this.CustomersManager = CustomersManager;
            this.Order_DishesManager = Order_DishesManager;
        }

        public ActionResult Index()
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdEmployee") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Stores the Id of the currently logged in employee in a variable
            int IdEmployee = HttpContext.Session.GetInt32("_IdEmployee").Value;

            List<Models.OrderDetailsEmployee> OrderDetails = new();

            //Creates a list that contains all open orders of the logged in employee
            List<Orders> Orders = OrdersManager.GetOpenOrdersEmployee(IdEmployee);
            Orders.OrderBy(x => x.DeliveryTime).ToList();

            //If the employee has no open orders, the empty list is simply transferred
            if (Orders == null)
            {
                return View(OrderDetails);
            }

            for (int i = 0; i < Orders.Count; i++)
            {
                int IdOrder = Orders[i].IdOrders;
                DateTime DeliveryTime = Orders[i].DeliveryTime;

                //Stores the last Order Id of the OrderDishes database table into the variable.
                Order_Dishes OrderDish = Order_DishesManager.GetOrderDishByOrderId(IdOrder);

                //All required data are stored in the ViewModel
                OrderDetailsEmployee OrderDetailsEmployee = new()
                {
                    RestaurantName = RestaurantsManager.GetRestaurantById(DishesManager.GetDishesById(OrderDish.IdDishes).IdRestaurant).Name,
                    ResaurantAddress = RestaurantsManager.GetRestaurantById(DishesManager.GetDishesById(OrderDish.IdDishes).IdRestaurant).Address,
                    RestaurantCity = VillagesManager.GetVillagesById(RestaurantsManager.GetRestaurantById(DishesManager.GetDishesById(OrderDish.IdDishes).IdRestaurant).IdVillage).PostalCode + " " + VillagesManager.GetVillagesById(RestaurantsManager.GetRestaurantById(DishesManager.GetDishesById(OrderDish.IdDishes).IdRestaurant).IdVillage).Name,
                    OrderId = IdOrder,
                    DeliveryTime = DeliveryTime
                };

                OrderDetails.Add(OrderDetailsEmployee);
            }

            return View(OrderDetails);
        }

        public ActionResult Details(int IdOrder)
        {
            CollectionOrderEmployee Model = new();

            //Returns the customer id of the respective order and stores it in the variable
            int IdCustomer = OrdersManager.GetOrderById(IdOrder).IdCustomers;

            List<Models.DishVM> Dishes = new();
            List<Order_Dishes> OrdersDishes = Order_DishesManager.GetOrderDishesByOrderId(IdOrder);

            OrderDetailsEmployee Order = new()
            {
                OrderId = IdOrder,
                CustomerLastname = CustomersManager.GetCustomerById(IdCustomer).Lastname,
                CustomerFirstname = CustomersManager.GetCustomerById(IdCustomer).Firstname,
                CustomerAddress = CustomersManager.GetCustomerById(IdCustomer).Address,
                CustomerVillage = VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(IdCustomer).IdVillage).PostalCode + " " + VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(IdCustomer).IdVillage).Name,
                CustomerPhoneNumber = CustomersManager.GetCustomerById(IdCustomer).PhoneNumber
            };

            //Returns all dishes of the respective order
            for (int i = 0; i < OrdersDishes.Count; i++)
            {
                Models.DishVM Dish = new()
                {
                    DishQuantity = OrdersDishes[i].Quantity,
                    DishName = DishesManager.GetDishesById(OrdersDishes[i].IdDishes).Name,
                    DishPrice = OrdersDishes[i].Quantity * DishesManager.GetDishesById(OrdersDishes[i].IdDishes).Price
                };
                Dishes.Add(Dish);
            }

            Model.OrderDetails = Order;
            Model.Dishes = Dishes;

            return View(Model);
        }

        [HttpPost]
        public ActionResult Delivered(int IdOrder)
        {
            //Updates the status to delivered
            OrdersManager.UpdateOrderStatus(IdOrder);
            return RedirectToAction(nameof(Index));
        }
    }
}
