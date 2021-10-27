using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IOrderStatusDB
    {
        List<OrderStatus> GetAllUserRoles();
        OrderStatus GetOrderStatusById(int idOrderStatus);
    }
}