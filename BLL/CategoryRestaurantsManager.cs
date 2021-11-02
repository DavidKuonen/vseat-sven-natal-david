using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
   public class CategoryRestaurantsManager
    {
        private ICategoryRestaurantsDB CategoryRestaurantsDb { get; }

        public CategoryRestaurantsManager(IConfiguration conf)
        {
            CategoryRestaurantsDb = new CategoryRestaurantsDB(conf);
        }

        public List<CategoryRestaurants> GetAllCateegoryRestautants()
        {
            return CategoryRestaurantsDb.GetAllCategoryRestaurants();
        }
    }
}
