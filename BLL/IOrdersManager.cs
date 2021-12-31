using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IOrdersManager
    {
        Orders AddOrder(Orders order);
        List<Orders> GetAllOrders();
        List<Orders> GetOrdersByCustomerId(int id);
        Orders GetOrdersById(int id);
        List<Orders> GetOrdersByStaffId(int id);
        void UpdateTotalPrice(int orderId);
        public int GetLastID();
    }
}