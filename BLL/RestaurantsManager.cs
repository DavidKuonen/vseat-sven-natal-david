using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class RestaurantsManager : IRestaurantsManager
    {
        private IRestaurantsDB RestaurantsDb { get; }

        public RestaurantsManager(IConfiguration conf)
        {
            RestaurantsDb = new RestaurantsDB(conf);
        }

        //SQL Befehle der DAL Klasse
        public List<Restaurants> GetAllRestaurants()
        {
            return RestaurantsDb.GetAllRestaurants();
        }

        public List<Restaurants> GetRestaurantsByCategoryRestaurant(int idCategoryRestaurant)
        {
            return RestaurantsDb.GetRestaurantsByCategoryRestaurant(idCategoryRestaurant);
        }
        public List<Restaurants> GetRestaurantsByDistrict(int idDistrict)
        {
            return RestaurantsDb.GetRestaurantsByDistrict(idDistrict);
        }
        public List<Restaurants> GetRestaurantsByName(string name)
        {
            return RestaurantsDb.GetRestaurantsByName(name);
        }
        public List<Restaurants> GetRestaurantsByVillage(int idVillage)
        {
            return RestaurantsDb.GetRestaurantsByVillage(idVillage);
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
        //SQL Befehle bis hier
    }
}
