using DTO;
using System.Collections.Generic;

namespace DAL
{
  public interface ICategoryDishesDB
  {
    List<CategoryDishes> GetAllCategoryDishes();
    CategoryDishes GetCategoryById(int id);
    CategoryDishes GetCategoryDishesByName(string name);
  }
}