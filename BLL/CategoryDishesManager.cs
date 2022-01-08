using DAL;
using DTO;

namespace BLL
{
    public class CategoryDishesManager : ICategoryDishesManager
    {
        private ICategoryDishesDB CategoryDishesDb { get; }

        public CategoryDishesManager(ICategoryDishesDB CategoryDishesDb)
        {
            this.CategoryDishesDb = CategoryDishesDb;
        }

        //SQL queries
        public CategoryDishes GetCategoryById(int id)
        {
            return CategoryDishesDb.GetCategoryById(id);
        }
    }
}
