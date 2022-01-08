
using DAL;
using DTO;
using System.Collections.Generic;

namespace BLL
{
    public class OrdersManager : IOrdersManager
    {
        private IOrdersDB OrdersDb { get; }
        private IOrder_DishesDB Order_DishesDb { get; }
        private IDishesDB DishesDb { get; }

        public OrdersManager(IOrdersDB OrdersDb, IOrder_DishesDB Order_DishesDb, IDishesDB DishesDb)
        {
            this.OrdersDb = OrdersDb;
            this.Order_DishesDb = Order_DishesDb;
            this.DishesDb = DishesDb;
        }

        //SQL queries
        public Orders AddOrder(Orders order)
        {
            return OrdersDb.AddOrder(order);
        }

        public Orders GetOrderById(int id)
        {
            return OrdersDb.GetOrderById(id);
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

        public List<Orders> GetOpenOrdersEmployee(int id)
        {
            return OrdersDb.GetOpenOrdersEmployee(id);
        }

        public List<Orders> GetOpenOrdersCustomer(int idCustomer)
        {
            return OrdersDb.GetOpenOrdersCustomer(idCustomer);
        }

        public List<Orders> GetPastOrdersCustomer(int idCustomer)
        {
            return OrdersDb.GetPastOrdersCustomer(idCustomer);
        }

        public void DeleteOrder(int orderId)
        {
            OrdersDb.DeleteOrder(orderId);
        }


        //SQL logic across multiple databases and DALs
        public void DeleteOrders(int idOrder)
        {
            //First deletes the data from the Orders_Dishes table, because this table contains a primary key from the Orders table
            Order_DishesDb.DeleteOrderDish(idOrder);
            DeleteOrder(idOrder);
        }

        public void UpdateTotalPrice(int orderId)
        {
            //First deletes the data from the Orders_Dishes table, because this table contains a primary key from the Orders table
            int quantity = 0;
            float preis = 0;
            float gesamt = 0;

            var orderdishes = Order_DishesDb.GetOrderDishesByOrderId(orderId);

            //Goes through all Dish orders and calculates the new price
            foreach (var orderdish in orderdishes)
            {
                quantity = orderdish.Quantity;
                preis = DishesDb.GetDishesById(orderdish.IdDishes).Price;
                gesamt += quantity * preis;
            }

            //Calls the DAL method that updates the Orders database table
            UpdateOrderPrice(orderId, gesamt);
        }
    }
}
