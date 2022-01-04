using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class CategoryRestaurantsManager : ICategoryRestaurantsManager
    {
        private ICategoryRestaurantsDB CategoryRestaurantsDb { get; }

        public CategoryRestaurantsManager(ICategoryRestaurantsDB CategoryRestaurantsDb)
        {
            this.CategoryRestaurantsDb = CategoryRestaurantsDb;
        }

        //SQL Befehle der DAL Klasse
       public CategoryRestaurants GetCategoryRestaurantsById(int id)
        {
            return CategoryRestaurantsDb.GetCategoryRestaurantsById(id);
        }

        public List<CategoryRestaurants> GetAllCateegoryRestautants()
        {
            return CategoryRestaurantsDb.GetAllCategoryRestaurants();
        }
        //SQL Befehle bis hier
    }
}
