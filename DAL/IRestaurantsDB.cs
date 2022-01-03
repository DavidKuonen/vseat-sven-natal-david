using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IRestaurantsDB
    {
        List<Restaurants> GetAllRestaurants();
        Restaurants GetRestaurantById(int idRestaurant);
        List<Restaurants> GetRestaurantsByCategoryRestaurant(int idCategoryRestaurant);
        List<Restaurants> GetRestaurantsByDistrict(int idDistrict);
        List<Restaurants> GetRestaurantsByName(string name);
        List<Restaurants> GetRestaurantsByVillage(int idVillage);
        Restaurants AddRestaurant(Restaurants restaurant);
        int DeleteRestaurant(int idRestaurant);

    }
}