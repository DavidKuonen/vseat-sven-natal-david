using DAL;
using DTO;
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

        //SQL queries
        public CategoryRestaurants GetCategoryRestaurantsById(int id)
        {
            return CategoryRestaurantsDb.GetCategoryRestaurantsById(id);
        }
    }
}
