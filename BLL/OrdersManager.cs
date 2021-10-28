
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
  class OrdersManager
  {
    private IOrdersDB OrdersDb { get; }

    public OrdersManager(IConfiguration conf)
    {
      OrdersDb = new OrdersDB(conf);
    }

    //SQL Befehle der DAL Klasse werden untenstehend geholt
    public Orders AddOrder(Orders order)
    {
      return OrdersDb.AddOrder(order);
    }

    public List<Orders> GetAllOrders()
    {
      return OrdersDb.GetAllOrders();
    }

    public Orders GetOrdersById(int id)
    {
      return OrdersDb.GetOrdersById(id);
    }
    //SQL Befehle bis hier


  }
}
