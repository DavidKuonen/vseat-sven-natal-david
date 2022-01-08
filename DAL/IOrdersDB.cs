using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IOrdersDB
    {
        Orders AddOrder(Orders order);
        Orders GetOrderById(int id);
        void UpdateOrderPrice(int orderId, float price);
        void UpdateOrderStatus(int orderId);
        void DeleteOrder(int idOrder);
        List<Orders> GetOpenOrdersCustomer(int idCustomer);
        public int GetLastID();
        List<Orders> GetOpenOrdersEmployee(int id);
    }
}