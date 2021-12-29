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

        public RestaurantController(IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IDistrictsManager DistrictsManager, ICategoryRestaurantsManager CategoryRestaurantsManager, IDishesManager DishesManager, ICategoryDishesManager CategoryDishesManager, IOrdersManager OrdersManager)
        {
            this.RestaurantsManager = RestaurantsManager;
            this.VillagesManager = VillagesManager;
            this.DistrictsManager = DistrictsManager;
            this.CategoryRestaurantsManager = CategoryRestaurantsManager;
            this.DishesManager = DishesManager;
            this.CategoryDishesManager = CategoryDishesManager;
            this.OrdersManager = OrdersManager;
        }

        public static List<Models.ShoppingCartVM> shoppingCartList = new List<ShoppingCartVM>();

        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IdCustomer") == null)
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
            /*if (HttpContext.Session.GetInt32("IdCustomer") == null)
            {
                return RedirectToAction("Index", "Login");
            }*/
           
            return View(shoppingCartList);
        }

        [HttpPost]
        public ActionResult ShoppingCart(ShoppingCartVM shoppingCartVM)
        {
            if(ModelState.IsValid)
            {
                var order = new DTO.Orders
                {
                    OrderTime = DateTime.Now,
                    FK_OrderStatus = 1,
                    FK_Customers = (int)HttpContext.Session.GetInt32("IdCustomer")
                };
            }
            return RedirectToAction(nameof(Confirmation));
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}
