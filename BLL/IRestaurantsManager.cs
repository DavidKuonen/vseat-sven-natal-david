using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IRestaurantsManager
    {
        Restaurants AddRestaurant(Restaurants restaurant);
        Restaurants GetRestaurantById(int idRestaurant);
        int DeleteRestaurant(int id);
        List<Restaurants> GetAllRestaurants();
        List<Restaurants> GetRestaurantsByCategoryRestaurant(int idCategoryRestaurant);
        List<Restaurants> GetRestaurantsByDistrict(int idDistrict);
        List<Restaurants> GetRestaurantsByName(string name);
        List<Restaurants> GetRestaurantsByVillage(int idVillage);
    }
}