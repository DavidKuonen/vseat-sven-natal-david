using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private IEmployeesManager EmployeesManager { get; }
        private IDishesManager DishesManager { get; }
        private IRestaurantsManager RestaurantsManager { get; }
        private IVillagesManager VillagesManager { get; }
        private IOrdersManager OrdersManager { get; }
        private ICustomersManager CustomersManager { get; }
        private IOrder_DishesManager Order_DishesManager { get; }

        public CustomerController(IEmployeesManager EmployeesManager, IDishesManager DishesManager, IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IOrdersManager OrdersManager, ICustomersManager CustomersManager, IOrder_DishesManager Order_DishesManager)
        {
            this.EmployeesManager = EmployeesManager;
            this.DishesManager = DishesManager;
            this.RestaurantsManager = RestaurantsManager;
            this.VillagesManager = VillagesManager;
            this.OrdersManager = OrdersManager;
            this.CustomersManager = CustomersManager;
            this.Order_DishesManager = Order_DishesManager;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            int idCustomer = HttpContext.Session.GetInt32("_IdCustomer").Value;

            List<Orders> orders = OrdersManager.GetOpenOrdersCustomer(idCustomer);

            return View(orders);
        }

        [HttpPost]
        public ActionResult DeleteOrder(int OrderID)
        {
            OrdersManager.DeleteOrders(OrderID);  
            return RedirectToAction("Index");
        }
    }
}
