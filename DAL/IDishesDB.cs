﻿using DTO;
using System.Collections.Generic;

namespace DAL
{
  public interface IDishesDB
  {
    List<Dishes> GetAllDishes();
    Dishes GetDishesById(int id);
    Dishes GetDishesByName(string name);
    Dishes AddDish(Dishes dish);

  }
}