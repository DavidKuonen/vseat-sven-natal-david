using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICategoryRestaurantsManager
    {
        List<CategoryRestaurants> GetAllCateegoryRestautants();
        CategoryRestaurants GetCategoryRestaurantsById(int id);
    }
}