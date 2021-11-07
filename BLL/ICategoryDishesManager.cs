using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICategoryDishesManager
    {
        List<CategoryDishes> GetAllCategoryDishes();
        CategoryDishes GetCategoryDishesByName(string name);
    }
}