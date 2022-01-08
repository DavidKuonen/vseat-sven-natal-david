using DAL;
using DTO;
using System.Collections.Generic;

namespace BLL
{
    public class RestaurantsManager : IRestaurantsManager
    {
        private IRestaurantsDB RestaurantsDb { get; }

        public RestaurantsManager(IRestaurantsDB RestaurantsDb)
        {
            this.RestaurantsDb = RestaurantsDb;
        }

        //SQL queries
        public List<Restaurants> GetAllRestaurants()
        {
            return RestaurantsDb.GetAllRestaurants();
        }

        public Restaurants GetRestaurantById(int idRestaurant)
        {
            return RestaurantsDb.GetRestaurantById(idRestaurant);
        }

        public Restaurants AddRestaurant(Restaurants restaurant)
        {
            return RestaurantsDb.AddRestaurant(restaurant);
        }

        public int DeleteRestaurant(int id)
        {
            return RestaurantsDb.DeleteRestaurant(id);
        }
    }
}
