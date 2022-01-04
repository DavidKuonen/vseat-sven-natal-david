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
    public class CategoryDishesManager : ICategoryDishesManager
    {
        private ICategoryDishesDB CategoryDishesDb { get; }

        public CategoryDishesManager(ICategoryDishesDB CategoryDishesDb)
        {
            this.CategoryDishesDb = CategoryDishesDb;
        }


        //SQL Befehle der DAL Klasse
        public List<CategoryDishes> GetAllCategoryDishes()
        {
            return CategoryDishesDb.GetAllCategoryDishes();
        }

        public CategoryDishes GetCategoryById(int id)
        {
            return CategoryDishesDb.GetCategoryById(id);
        }

        public CategoryDishes GetCategoryDishesByName(string name)
        {
            return CategoryDishesDb.GetCategoryDishesByName(name);
        }
    }
}
