using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        private IVillagesManager VillagesManager { get; }
        private IDistrictsManager DistrictsManager { get; }
        private IOrdersManager OrdersManager { get; }
        private ICustomersManager CustomersManager { get; }
        private IOrder_DishesManager Order_DishesManager { get; }
        private IEmployeesManager EmployeesManager { get; }

        public OrderController(IVillagesManager VillagesManager, IDistrictsManager DistrictsManager, IOrdersManager OrdersManager, ICustomersManager CustomersManager, IOrder_DishesManager Order_DishesManager, IEmployeesManager EmployeesManager)
        {
            this.VillagesManager = VillagesManager;
            this.DistrictsManager = DistrictsManager;
            this.OrdersManager = OrdersManager;
            this.CustomersManager = CustomersManager;
            this.Order_DishesManager = Order_DishesManager;
            this.EmployeesManager = EmployeesManager;
        }

        public ActionResult Confirmation()
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            //Create an object from the CollectionDataModel
            CollectionDataModel model = new();

            //Creates the object from the PersonalDetails model and is filled with values
            Models.PersonalDetails personalDetails = new()
            {
                NameCustomer = CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Firstname + " " + CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Lastname,
                AddressCustomer = CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Address,
                CityCustomer = VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdVillage).PostalCode + " " + VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdVillage).Name,
                DistrictCustomer = DistrictsManager.GetDistrictsById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdDistrict).Name,
            };

            //Adds the associated objects to the CollectionDataModel.
            model.PersonalDetails = personalDetails;
            model.ShoppingCartVMs = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");

            //Passes the model of the view
            return View(model);
        }

        [HttpPost]
        public ActionResult Order(DateTime DeliveryTime)
        {
            //Declaring the Order Id and the Employee Id
            int OrderId = 0;
            int EmployeeId = EmployeesManager.GetTheRightEmployee(HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List")[0].RestaurantId, DeliveryTime);

            if (EmployeeId == 0)
            {
                return RedirectToAction("NoEmployee");
            }

            //Create DTO object and fill it with values 
            DTO.Orders Order = new()
            {
                OrderTime = DateTime.Now,
                DeliveryTime = DeliveryTime,
                TotalPrice = 0,
                IdOrderStatus = 1,
                IdEmployee = EmployeeId,
                IdCustomers = HttpContext.Session.GetInt32("_IdCustomer").Value
            };

            //Save order into the database
            OrdersManager.AddOrder(Order);

            //From the last inserted order to the database and bring back the ID of this order and store it in a variable
            OrderId = OrdersManager.GetLastID();

            //In the database table Orders_Dishes store all dishes from the shopping cart to the corresponding ID of the order. 
            for (int i = 0; i < HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List").Count; i++)
            {
                var OrderDish = new DTO.Order_Dishes
                {
                    Quantity = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List")[i].DishQuantity,
                    IdDishes = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List")[i].DishId,
                    IdOrders = OrderId
                };

                Order_DishesManager.AddOrderDishes(OrderDish);
            }

            //Update the price of the entire order in the Orders table
            OrdersManager.UpdateTotalPrice(OrderId);

            //Because the order is completed, clear the list
            List<ShoppingCartVM> Cart = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");
            Cart.Clear();

            //Reset the list in the session
            HttpContext.Session.SetComplexData("_List", Cart);
            HttpContext.Session.SetInt32("_CountList", Cart.Count);

            //Forwarding to the thank you page
            return RedirectToAction("ThankYou", new { IdOrder = OrderId });
        }

        public ActionResult ThankYou(int IdOrder)
        {
            //Fetches the order to the matching Id
            var Order = OrdersManager.GetOrderById(IdOrder);
            return View(Order);
        }

        public ActionResult NoEmployee()
        {
            return View();
        }
    }
}
