using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IOrdersManager
    {
        Orders AddOrder(Orders order);
        //List<Orders> GetAllOrders();
        //List<Orders> GetOrdersByCustomerId(int id);
        Orders GetOrderById(int id);
        //List<Orders> GetOrdersByStaffId(int id);
        void UpdateTotalPrice(int orderId);
        void UpdateOrderStatus(int orderId);
        //int GetOrdersNotDelivered(int idEmployee, DateTime DeliveryTime);
        public int GetLastID();
        List<Orders> GetOpenOrdersEmployee(int id);
        List<Orders> GetOpenOrdersCustomer(int idCustomer);
        //List<Orders> GetPastOrdersCustomer(int idCustomer);
        void DeleteOrders(int orderId);
    }
}