using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IOrder_DishesDB
    {
        Order_Dishes AddOrderDishes(Order_Dishes orderdishes);
        //List<Order_Dishes> GetAllOrder_Dishes();
        //Order_Dishes GetOrderDishesById(int id);
        List<Order_Dishes> GetOrderDishesByOrderId(int id);
        Order_Dishes GetOrderDishByOrderId(int id);
        void DeleteOrderDish(int idOrder);
    }
}