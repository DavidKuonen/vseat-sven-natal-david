using DAL;
using DTO;
using System.Collections.Generic;

namespace BLL
{
    public class Order_DishesManager : IOrder_DishesManager
    {
        private IOrder_DishesDB Order_DishesDb { get; }

        public Order_DishesManager(IOrder_DishesDB Order_DishesDb)
        {
            this.Order_DishesDb = Order_DishesDb;
        }

        //SQL queries
        public Order_Dishes AddOrderDishes(Order_Dishes orderdishes)
        {
            return Order_DishesDb.AddOrderDishes(orderdishes);
        }

        public List<Order_Dishes> GetOrderDishesByOrderId(int id)
        {
            return Order_DishesDb.GetOrderDishesByOrderId(id);
        }

        public Order_Dishes GetOrderDishByOrderId(int id)
        {
            return Order_DishesDb.GetOrderDishByOrderId(id);
        }

        public void DeleteOrderDish(int idOrder)
        {
            Order_DishesDb.DeleteOrderDish(idOrder);
        }
    }
}
