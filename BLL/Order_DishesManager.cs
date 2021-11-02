using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  class Order_DishesManager
  {
    private IOrder_DishesDB Order_DishesDb { get; }
  
    public Order_DishesManager(IConfiguration conf)
    {
      Order_DishesDb = new Order_DishesDB(conf);
    }

    //SQL Befehle der DAL Klasse werden untenstehend geholt

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

    //SQL Befehle bis hier









  }
}
