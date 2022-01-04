using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }

    public class RestaurantController : Controller
    {
        private IRestaurantsManager RestaurantsManager { get; }
        private IVillagesManager VillagesManager { get; }
        private IDistrictsManager DistrictsManager { get; }
        private ICategoryRestaurantsManager CategoryRestaurantsManager { get; }
        private IDishesManager DishesManager { get; }
        private ICategoryDishesManager CategoryDishesManager { get; }

        public RestaurantController(IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IDistrictsManager DistrictsManager, ICategoryRestaurantsManager CategoryRestaurantsManager, IDishesManager DishesManager, ICategoryDishesManager CategoryDishesManager)
        {
            this.RestaurantsManager = RestaurantsManager;
            this.VillagesManager = VillagesManager;
            this.DistrictsManager = DistrictsManager;
            this.CategoryRestaurantsManager = CategoryRestaurantsManager;
            this.DishesManager = DishesManager;
            this.CategoryDishesManager = CategoryDishesManager;
        }

        //public static Models.Session SessionExtensions = new();
        public static List<Models.ShoppingCartVM> shoppingCartList = new();

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
                dish.DishImage = DishesManager.GetDishesByRestaurantId(idRestaurant)[i].Image;
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
            List<ShoppingCartVM> newList = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");

            var cart = new Models.ShoppingCartVM
            {
                DishQuantity = dishVM.DishQuantity,
                DishId = dishVM.DishId,
                DishName = dishVM.DishName,
                Price = dishVM.DishPrice,
                PriceDishTotal = dishVM.DishQuantity * dishVM.DishPrice,
                CustomerId = 1,
                RestaurantId = dishVM.RestaurantId
            };
            
            if (newList == null)
            {
                shoppingCartList.Clear();
            }
            else if(newList != shoppingCartList)
            {
                shoppingCartList = newList;
            }

            shoppingCartList.Add(cart);
            HttpContext.Session.SetComplexData("_List", shoppingCartList);

            return RedirectToAction("Dishes", new { idRestaurant = dishVM.RestaurantId});
        }
    }
}
