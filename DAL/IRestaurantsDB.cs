using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IRestaurantsDB
    {
        List<Restaurants> GetAllRestaurants();
        List<Restaurants> GetRestaurantsByCategoryRestaurant(int idCategoryRestaurant);
        List<Restaurants> GetRestaurantsByDistrict(int idDistrict);
        List<Restaurants> GetRestaurantsByName(string name);
        List<Restaurants> GetRestaurantsByVillage(int idVillage);
    }
}