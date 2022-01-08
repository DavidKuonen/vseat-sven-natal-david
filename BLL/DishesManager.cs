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
    public class DishesManager : IDishesManager
    {
        private IDishesDB DishesDb { get; }

        public DishesManager(IDishesDB DishesDb)
        {
            this.DishesDb = DishesDb;
        }

        //SQL queries
        public List<Dishes> GetAllDishes()
        {
            return DishesDb.GetAllDishes();
        }

        public List<Dishes> GetDishesByRestaurantId(int idRestaurant)
        {
            return DishesDb.GetDishesByRestaurantId(idRestaurant);
        }

        public Dishes GetDishesById(int idDish)
        {
            return DishesDb.GetDishesById(idDish);
        }
    }
}
