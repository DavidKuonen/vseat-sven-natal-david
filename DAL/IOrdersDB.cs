﻿using DTO;
using System.Collections.Generic;

namespace DAL
{
  public interface IOrdersDB
  {
    Orders AddOrder(Orders order);
    List<Orders> GetAllOrders();
    Orders GetOrdersById(int id);
  }
}