using DTO;
using System.Collections.Generic;

namespace DAL
{
  interface IDishesDB
  {
    List<Dishes> GetAllDishes();
    Dishes GetDishesByName(string name);
  }
}