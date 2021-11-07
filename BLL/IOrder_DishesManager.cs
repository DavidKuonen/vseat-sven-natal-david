using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IOrder_DishesManager
    {
        Order_Dishes AddOrderDishes(Order_Dishes orderdishes);
        List<Order_Dishes> GetAllOrder_Dishes();
        Order_Dishes GetOrderDishesById(int id);
    }
}