using DTO;

namespace DAL
{
    public interface ICategoryDishesDB
    {
        //List<CategoryDishes> GetAllCategoryDishes();
        CategoryDishes GetCategoryById(int id);
        //CategoryDishes GetCategoryDishesByName(string name);
    }
}