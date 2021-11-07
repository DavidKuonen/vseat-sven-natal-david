using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IDishesManager
    {
        List<Dishes> GetAllDishes();
        Dishes GetDishesByName(string name);
    }
}