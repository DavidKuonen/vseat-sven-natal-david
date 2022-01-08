using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IOrdersManager
    {
        Orders AddOrder(Orders order);
        Orders GetOrderById(int id);
        void UpdateTotalPrice(int orderId);
        void UpdateOrderStatus(int orderId);
        public int GetLastID();
        List<Orders> GetOpenOrdersEmployee(int id);
        List<Orders> GetOpenOrdersCustomer(int idCustomer);
        void DeleteOrders(int orderId);
    }
}