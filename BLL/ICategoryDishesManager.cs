using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICategoryDishesManager
    {
        List<CategoryDishes> GetAllCategoryDishes();
        CategoryDishes GetCategoryById(int id);
        CategoryDishes GetCategoryDishesByName(string name);
    }
}