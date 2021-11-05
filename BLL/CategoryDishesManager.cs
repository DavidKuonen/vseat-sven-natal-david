using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class CategoryDishesManager
  {
    private ICategoryDishesDB CategoryDishesDb { get; }

    public CategoryDishesManager(IConfiguration conf)
    {
      CategoryDishesDb = new CategoryDishesDB(conf);
    }


    //SQL Befehle der DAL Klasse
    public List<CategoryDishes> GetAllCategoryDishes()
    {
      return CategoryDishesDb.GetAllCategoryDishes();
    }

    public CategoryDishes GetCategoryDishesByName(string name)
    {
      return CategoryDishesDb.GetCategoryDishesByName(name);
    }
    //SQL Befehle bis hier

  }
}
