
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
    public class OrdersManager : IOrdersManager
    {
        private IOrdersDB OrdersDb { get; }
        private IOrder_DishesDB Order_DishesDb { get; }
        private IDishesDB DishesDb { get; }
        private IEmployeesDB EmployeesDb { get; }

        public OrdersManager(IConfiguration conf)
        {
            OrdersDb = new OrdersDB(conf);
            Order_DishesDb = new Order_DishesDB(conf);
            DishesDb = new DishesDB(conf);
            EmployeesDb = new EmployeesDB(conf);
        }

        //SQL Befehle der DAL Klasse
        public Orders AddOrder(Orders order)
        {
            return OrdersDb.AddOrder(order);
        }

        public List<Orders> GetAllOrders()
        {
            return OrdersDb.GetAllOrders();
        }

        public Orders GetOrderById(int id)
        {
            return OrdersDb.GetOrderById(id);
        }

        public List<Orders> GetOrdersByCustomerId(int id)
        {
            return OrdersDb.GetOrdersByCustomerId(id);
        }

        public List<Orders> GetOrdersByStaffId(int id)
        {
            return OrdersDb.GetOrdersByStaffId(id);
        }

        public void UpdateOrderPrice(int orderId, float price)
        {
            OrdersDb.UpdateOrderPrice(orderId, price);
        }

        public void UpdateOrderStatus(int orderId)
        {
            OrdersDb.UpdateOrderStatus(orderId);
        }

        public int GetLastID()
        {
            return OrdersDb.GetLastID();
        }

        public int GetOrdersNotDelivered(int idEmployee, DateTime DeliveryTime)
        {
            return OrdersDb.GetOrdersNotDelivered(idEmployee, DeliveryTime);
        }

        public List<Orders> GetOpenOrdersEmployee(int id)
        {
            return OrdersDb.GetOpenOrdersEmployee(id);
        }
        //SQL Befehle bis hier

        public void UpdateTotalPrice(int orderId)
        {
            int quantity = 0;
            float preis = 0;
            float gesamt = 0;

            var orderdishes = Order_DishesDb.GetOrderDishesByOrderId(orderId);

            foreach (var orderdish in orderdishes)
            {
                quantity = orderdish.Quantity;
                preis = DishesDb.GetDishesById(orderdish.FK_Dishes).price;
                gesamt += quantity * preis;
            }

            UpdateOrderPrice(orderId, gesamt);
        }
    }
}
