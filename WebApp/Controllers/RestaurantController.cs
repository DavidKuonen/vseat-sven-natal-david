using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public RestaurantController(IRestaurantsManager RestaurantsManager, IVillagesManager VillagesManager, IDistrictsManager DistrictsManager, ICategoryRestaurantsManager CategoryRestaurantsManager, IDishesManager DishesManager, ICategoryDishesManager CategoryDishesManager)
        {
            this.RestaurantsManager = RestaurantsManager;
            this.VillagesManager = VillagesManager;
            this.DistrictsManager = DistrictsManager;
            this.CategoryRestaurantsManager = CategoryRestaurantsManager;
            this.DishesManager = DishesManager;
            this.CategoryDishesManager = CategoryDishesManager;
        }

        //Creates a static list for the shopping cart
        public static List<Models.ShoppingCartVM> ShoppingCartList = new();

        public ActionResult Index()
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Creates a list from the restaurant view model
            List<Models.RestaurantVM> Restaurants = new();

            for (int i = 0; i < RestaurantsManager.GetAllRestaurants().Count; i++)
            {
                //Creates a RestaurantVM object and fills the setters with values. For this it iterates through a list of all restaurants
                Models.RestaurantVM Restaurant = new()
                {
                    RestaurantId = RestaurantsManager.GetAllRestaurants()[i].IdRestaurant,
                    RestaurantName = RestaurantsManager.GetAllRestaurants()[i].Name,
                    ResaurantAddress = RestaurantsManager.GetAllRestaurants()[i].Address,
                    RestaurantCity = VillagesManager.GetVillagesById(RestaurantsManager.GetAllRestaurants()[i].IdVillage).Name,
                    RestaurantDistrict = DistrictsManager.GetDistrictsById(RestaurantsManager.GetAllRestaurants()[i].IdDistrict).Name,
                    RestaurantCategory = CategoryRestaurantsManager.GetCategoryRestaurantsById(RestaurantsManager.GetAllRestaurants()[i].IdCategoryRestaurant).Name,
                    RestaurantImage = RestaurantsManager.GetAllRestaurants()[i].RestaurantImage
                };
                Restaurants.Add(Restaurant);
            }

            return View(Restaurants);
        }

        public ActionResult Dishes(int IdRestaurant)
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Creates a list of the dish view model
            List<Models.DishVM> DishVM = new();

            for (int i = 0; i < DishesManager.GetDishesByRestaurantId(IdRestaurant).Count; i++)
            {
                Models.DishVM Dish = new()
                {
                    DishImage = DishesManager.GetDishesByRestaurantId(IdRestaurant)[i].Image,
                    DishId = DishesManager.GetDishesByRestaurantId(IdRestaurant)[i].IdDishes,
                    DishName = DishesManager.GetDishesByRestaurantId(IdRestaurant)[i].Name,
                    DishPrice = DishesManager.GetDishesByRestaurantId(IdRestaurant)[i].Price,
                    DishCalories = DishesManager.GetDishesByRestaurantId(IdRestaurant)[i].Calories,
                    DishCategory = CategoryDishesManager.GetCategoryById(DishesManager.GetDishesByRestaurantId(IdRestaurant)[i].IdCategoryDishes).Name,
                    RestaurantId = IdRestaurant
                };
                DishVM.Add(Dish);
            }

            return View(DishVM);
        }

        [HttpPost]
        public ActionResult Dishes(DishVM DishVM)
        {
            //Get the list from the session
            List<ShoppingCartVM> NewCartList = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");

            //Saves the passed dishVM parameter into an object of type ShoppingCartViewModel
            var Cart = new Models.ShoppingCartVM
            {
                DishQuantity = DishVM.DishQuantity,
                DishId = DishVM.DishId,
                DishName = DishVM.DishName,
                Price = DishVM.DishPrice,
                PriceDishTotal = DishVM.DishQuantity * DishVM.DishPrice,
                CustomerId = (int)HttpContext.Session.GetInt32("_IdCustomer"),
                RestaurantId = DishVM.RestaurantId
            };

            //Checks if the Session List is null, if it is, then the contents of the ShoppingCartList are deleted.
            //This is how an error message does not occur
            //If the SessionList is not null the shoppingCartList takes over the content of the list of the session
            //So both lists are on the same level
            if (NewCartList == null)
            {
                ShoppingCartList.Clear();
            }
            else if (NewCartList != ShoppingCartList)
            {
                ShoppingCartList = NewCartList;
            }

            //Dish is added to the shopping cart
            ShoppingCartList.Add(Cart);
            //List is passed to the session
            HttpContext.Session.SetComplexData("_List", ShoppingCartList);
            //List number is passed to the session, so that you can see in the navigation how much is in the shopping cart
            HttpContext.Session.SetInt32("_CountList", ShoppingCartList.Count);

            //Redirection to the same page
            return RedirectToAction("Dishes", new { IdRestaurant = DishVM.RestaurantId });
        }
    }
}
