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
    public class RestaurantController : Controller
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

        public RestaurantController(IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IDistrictsManager DistrictsManager, ICategoryRestaurantsManager CategoryRestaurantsManager, IDishesManager DishesManager, ICategoryDishesManager CategoryDishesManager, IOrdersManager OrdersManager, ICustomersManager CustomersManager, IOrder_DishesManager Order_DishesManager)
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
        }

        public static List<Models.ShoppingCartVM> shoppingCartList = new List<ShoppingCartVM>();

        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            List<Models.RestaurantVM> restaurants = new List<Models.RestaurantVM>();

            for (int i = 0; i < RestaurantsManager.GetAllRestaurants().Count; i++)
            {
                Models.RestaurantVM resti = new();
                resti.RestaurantId = RestaurantsManager.GetAllRestaurants()[i].idRestaurant;
                resti.RestaurantName = RestaurantsManager.GetAllRestaurants()[i].name;
                resti.ResaurantAddress = RestaurantsManager.GetAllRestaurants()[i].address;
                resti.RestaurantCity = VillagesManager.GetVillagesById(RestaurantsManager.GetAllRestaurants()[i].idVillage).name;
                resti.RestaurantDistrict = DistrictsManager.GetDistrictsById(RestaurantsManager.GetAllRestaurants()[i].idDistrict).name;
                resti.RestaurantCategory = CategoryRestaurantsManager.GetCategoryRestaurantsById(RestaurantsManager.GetAllRestaurants()[i].idCategoryRestaurant).name;
                restaurants.Add(resti);
            }

            return View(restaurants);
        }

        public ActionResult Dishes(int idRestaurant)
        {
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            List<Models.DishVM> dishVM = new List<Models.DishVM>();

            for (int i = 0; i < DishesManager.GetDishesByRestaurantId(idRestaurant).Count; i++)
            {
                Models.DishVM dish = new();
                dish.DishId = DishesManager.GetDishesByRestaurantId(idRestaurant)[i].idDishes;
                dish.DishName = DishesManager.GetDishesByRestaurantId(idRestaurant)[i].name;
                dish.DishPrice = DishesManager.GetDishesByRestaurantId(idRestaurant)[i].price;
                dish.DishCalories = DishesManager.GetDishesByRestaurantId(idRestaurant)[i].calories;
                dish.DishCategory = CategoryDishesManager.GetCategoryById(DishesManager.GetDishesByRestaurantId(idRestaurant)[i].FK_CategoryDishes).name;
                dish.RestaurantId = idRestaurant;
                dishVM.Add(dish);
            }

            return View(dishVM);
        }

        [HttpPost]
        public ActionResult Dishes(DishVM dishVM)
        {

            if (ModelState.IsValid)
            {
                var cart = new Models.ShoppingCartVM
                {
                    DishId = dishVM.DishId,
                    DishName = dishVM.DishName,
                    Price = dishVM.DishPrice,
                    CustomerId = 1,
                    RestaurantId = dishVM.RestaurantId
                };

                shoppingCartList.Add(cart);
                return RedirectToAction(nameof(ShoppingCart));
            }

            return View(dishVM);
        }

        public ActionResult ShoppingCart()
        {
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("index", "Login");
            }

            return View(shoppingCartList);
        }

        [HttpPost]
        public ActionResult ShoppingCartView()
        {
            return RedirectToAction(nameof(Confirmation));
        }

        public ActionResult Confirmation()
        {

            CollectionDataModell model = new CollectionDataModell();

            Models.PersonalDetails personalDetails = new()
            {
                NameCustomer = CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Firstname + " " + CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Lastname,
                AddressCustomer = CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).Address,
                CityCustomer = VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdVillage).postalCode + " " + VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdVillage).name,
                DistrictCustomer = DistrictsManager.GetDistrictsById(CustomersManager.GetCustomerById(HttpContext.Session.GetInt32("_IdCustomer").Value).IdDistrict).name,
            };

            model.personalDetails = personalDetails;
            model.shoppingCartVMs = shoppingCartList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Order()
        {
            /*float TotalPrice = 0;

              foreach (ShoppingCartVM sc in shoppingCartList)
              {
                  TotalPrice += sc.Price;
              }*/
            int orderId;

            if (ModelState.IsValid)
            {
                var order = new DTO.Orders
                {
                    OrderTime = DateTime.Now,
                    DeliveryTime = DateTime.Now,
                    TotalPrice = 0,
                    FK_OrderStatus = 1,
                    FK_Staff = 1,
                    FK_Customers = HttpContext.Session.GetInt32("_IdCustomer").Value
                };

                OrdersManager.AddOrder(order);

                orderId = OrdersManager.GetLastID();

                for (int i = 0; i < shoppingCartList.Count; i++)
                {
                    var orderDish = new DTO.Order_Dishes
                    {
                        Quantity = 2,
                        FK_Dishes = shoppingCartList[i].DishId,
                        FK_Orders = orderId
                    };
                    Order_DishesManager.AddOrderDishes(orderDish);
                    OrdersManager.UpdateTotalPrice(orderId);
                }
            }




            return RedirectToAction(nameof(Index));
        }
    }
}
