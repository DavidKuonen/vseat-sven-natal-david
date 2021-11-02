using DTO;
using System.Collections.Generic;

namespace DAL
{
  public interface ICategoryDishesDB
  {
    List<CategoryDishes> GetAllCategoryDishes();
    CategoryDishes GetCategoryDishesByName(string name);
  }
}