using DTO;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Order_DishesManager : IOrder_DishesManager
    {
        private IOrder_DishesDB Order_DishesDb { get; }

        public Order_DishesManager(IOrder_DishesDB Order_DishesDb)
        {
            this.Order_DishesDb = Order_DishesDb;
        }

        //SQL Befehle der DAL Klasse
        public Order_Dishes AddOrderDishes(Order_Dishes orderdishes)
        {
            return Order_DishesDb.AddOrderDishes(orderdishes);
        }

        public List<Order_Dishes> GetAllOrder_Dishes()
        {
            return Order_DishesDb.GetAllOrder_Dishes();
        }

        public Order_Dishes GetOrderDishesById(int id)
        {
            return Order_DishesDb.GetOrderDishesById(id);
        }

        public List<Order_Dishes> GetOrderDishesByOrderId(int id)
        {
            return Order_DishesDb.GetOrderDishesByOrderId(id);
        }

        public void DeleteOrderDish(int idOrder)
        {
            Order_DishesDb.DeleteOrderDish(idOrder);
        }

        //SQL Befehle bis hier
    }
}
