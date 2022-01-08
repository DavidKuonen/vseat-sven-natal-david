using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface ICategoryRestaurantsDB
    {
        //List<CategoryRestaurants> GetAllCategoryRestaurants();
        CategoryRestaurants GetCategoryRestaurantsById(int id);
    }
}