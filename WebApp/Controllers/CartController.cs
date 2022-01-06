using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private IRestaurantsManager RestaurantsManager { get; }
        private IVillagesManager VillagesManager { get; }
        private IDistrictsManager DistrictsManager { get; }
        private ICategoryRestaurantsManager CategoryRestaurantsManager { get; }
        private IDishesManager DishesManager { get; }
        private ICategoryDishesManager CategoryDishesManager { get; }
        private IOrdersManager OrdersManager { get; }
        private ICustomersManager CustomersManager { get; }
        private IOrder_DishesManager Order_DishesManager { get; }
        private IEmployeesManager EmployeesManager { get; }

        public CartController(IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IDistrictsManager DistrictsManager, ICategoryRestaurantsManager CategoryRestaurantsManager, IDishesManager DishesManager, ICategoryDishesManager CategoryDishesManager, IOrdersManager OrdersManager, ICustomersManager CustomersManager, IOrder_DishesManager Order_DishesManager, IEmployeesManager EmployeesManager)
        {
            this.RestaurantsManager = RestaurantsManager;
            this.VillagesManager = VillagesManager;
            this.DistrictsManager = DistrictsManager;
            this.CategoryRestaurantsManager = CategoryRestaurantsManager;
            this.DishesManager = DishesManager;
            this.CategoryDishesManager = CategoryDishesManager;
            this.OrdersManager = OrdersManager;
            this.CustomersManager = CustomersManager;
            this.Order_DishesManager = Order_DishesManager;
            this.EmployeesManager = EmployeesManager;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            return View(HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List"));
        }


        [HttpPost]
        public ActionResult DeleteDishesCart(int DishId)
        {
            List<ShoppingCartVM> cart = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");
            var RemoveItem = cart.Single(cart => cart.DishId == DishId);
            cart.Remove(RemoveItem);
            HttpContext.Session.SetComplexData("_List", cart);

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult ShoppingCartView()
        {
            return RedirectToAction(nameof(Confirmation));
        }

        public ActionResult Confirmation()
        {
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            CollectionDataModel model = new CollectionDataModel();

            Models.PersonalDetails personalDetails = new()
            {
                NameCustomer = CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Firstname + " " + CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Lastname,
                AddressCustomer = CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Address,
                CityCustomer = VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdVillage).postalCode + " " + VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdVillage).name,
                DistrictCustomer = DistrictsManager.GetDistrictsById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdDistrict).name,
            };

            model.personalDetails = personalDetails;
            model.shoppingCartVMs = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");

            return View(model);
        }

        [HttpPost]
        public ActionResult Order(DateTime DeliveryTime)
        {
            int orderId = 0;
            int employeeId = EmployeesManager.GetTheRightEmployee(HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List")[0].RestaurantId, DeliveryTime);

            if(employeeId == 0)
            {
                return RedirectToAction("NoEmployee");
            }

            if (ModelState.IsValid)
            {
                var order = new DTO.Orders
                {
                    OrderTime = DateTime.Now,
                    DeliveryTime = DeliveryTime,
                    TotalPrice = 0,
                    FK_OrderStatus = 1,
                    FK_Staff = employeeId,
                    FK_Customers = HttpContext.Session.GetInt32("_IdCustomer").Value
                };

                OrdersManager.AddOrder(order);

                orderId = OrdersManager.GetLastID();

                for (int i = 0; i < HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List").Count; i++)
                {
                    var orderDish = new DTO.Order_Dishes
                    {
                        Quantity = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List")[i].DishQuantity,
                        FK_Dishes = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List")[i].DishId,
                        FK_Orders = orderId
                    };
                    Order_DishesManager.AddOrderDishes(orderDish);
                }

                OrdersManager.UpdateTotalPrice(orderId);

                List<ShoppingCartVM> cart = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");
                cart.Clear();
                HttpContext.Session.SetComplexData("_List", cart);
            }

            return RedirectToAction("ThankYou", new { idOrder = orderId });
        }

        public ActionResult ThankYou(int idOrder)
        {
            var order = OrdersManager.GetOrderById(idOrder);
            return View(order);
        }

        public ActionResult NoEmployee()
        {
            return View();
        }
    }
}
