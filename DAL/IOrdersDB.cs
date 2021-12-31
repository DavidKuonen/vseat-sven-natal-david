using DTO;
using System.Collections.Generic;

namespace DAL
{
  public interface IOrdersDB
  {
    Orders AddOrder(Orders order);
    List<Orders> GetAllOrders();
    Orders GetOrdersById(int id);
    List<Orders> GetOrdersByStaffId(int id);
    List<Orders> GetOrdersByStaffIdAndCustomerId(int idStaff, int idEmployee);
    List<Orders> GetOrdersByCustomerId(int id);
    void UpdateOrderPrice(int orderId, float price);
    int GetOrderIdByCustomerId(int idCustomer, int idEmployee);
    public int GetLastID();
    }
}