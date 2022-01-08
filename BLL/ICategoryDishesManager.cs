using DTO;

namespace BLL
{
    public interface ICategoryDishesManager
    {
        //List<CategoryDishes> GetAllCategoryDishes();
        CategoryDishes GetCategoryById(int id);
        //CategoryDishes GetCategoryDishesByName(string name);
    }
}