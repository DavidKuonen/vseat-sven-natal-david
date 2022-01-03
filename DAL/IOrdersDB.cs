using DTO;
using System;
using System.Collections.Generic;

namespace DAL
{
  public interface IOrdersDB
  {
    Orders AddOrder(Orders order);
    List<Orders> GetAllOrders();
    Orders GetOrderById(int id);
    List<Orders> GetOrdersByStaffId(int id);
    List<Orders> GetOrdersByStaffIdAndCustomerId(int idStaff, int idEmployee);
    List<Orders> GetOrdersByCustomerId(int id);
    void UpdateOrderPrice(int orderId, float price);
    void UpdateOrderStatus(int orderId);
    int GetOrderIdByCustomerId(int idCustomer, int idEmployee);
    int GetOrdersNotDelivered(int idEmployee, DateTime DeliveryTime);
    public int GetLastID();
    List<Orders> GetOpenOrdersEmployee(int id);
    }
}