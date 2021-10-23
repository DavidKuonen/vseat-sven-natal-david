using DTO;
using System.Collections.Generic;

namespace DAL
{
  interface ICategoryDishesDB
  {
    List<CategoryDishes> GetAllCategoryDishes();
    CategoryDishes GetCategoryDishesByName(string name);
  }
}