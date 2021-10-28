using System.Collections.Generic;

namespace DAL
{
  public interface IOrder_DishesDB
  {
    Order_Dishes AddOrderDishes(Order_Dishes orderdishes);
    List<Order_Dishes> GetAllOrder_Dishes();
    Order_Dishes GetOrderDishesById(int id);
  }
}