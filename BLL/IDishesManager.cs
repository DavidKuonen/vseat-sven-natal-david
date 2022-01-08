using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IDishesManager
    {
        List<Dishes> GetAllDishes();
        List<Dishes> GetDishesByRestaurantId(int idRestaurant);
        Dishes GetDishesById(int id);
        //Dishes GetDishesByName(string name);
    }
}