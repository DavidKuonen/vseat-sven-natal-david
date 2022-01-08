using DTO;

namespace BLL
{
    public interface ICategoryRestaurantsManager
    {
        //List<CategoryRestaurants> GetAllCateegoryRestautants();
        CategoryRestaurants GetCategoryRestaurantsById(int id);
    }
}