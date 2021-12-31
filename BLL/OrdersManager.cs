
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

        public OrdersManager(IConfiguration conf)
        {
            OrdersDb = new OrdersDB(conf);
            Order_DishesDb = new Order_DishesDB(conf);
            DishesDb = new DishesDB(conf);
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

        public Orders GetOrdersById(int id)
        {
            return OrdersDb.GetOrdersById(id);
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

        public int GetLastID()
        {
            return OrdersDb.GetLastID();
        }
        //SQL Befehle bis hier

        public void UpdateTotalPrice(int orderId)
        {
            int quantity = 0;
            float preis = 0;
            float gesamt = 0;

            List<Order_Dishes> orderdishes = Order_DishesDb.GetOrderDishesByOrderId(orderId);

            foreach (var orderdish in orderdishes)
            {

                quantity = orderdish.Quantity;
                Dishes dish = DishesDb.GetDishesById(orderdish.FK_Dishes);
                preis = dish.price;
                gesamt = quantity * preis;
            }


            UpdateOrderPrice(orderId, gesamt);
        }
    }
}
