using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class Order_DishesDB : IOrder_DishesDB
    {
        private IConfiguration Configuration { get; }

        public Order_DishesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Order_Dishes> GetOrderDishesByOrderId(int idOrder)
        {
            List<Order_Dishes> orders = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders_Dishes WHERE idOrder = @idOrder";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (orders == null)
                                orders = new List<Order_Dishes>();

                            Order_Dishes order = new Order_Dishes();

                            order.IdOrder_Dishes = (int)reader["idOrder_Dish"];

                            order.Quantity = (int)reader["quantity"];

                            order.IdDishes = (int)reader["idDish"];

                            order.IdOrders = (int)reader["idOrder"];

                            orders.Add(order);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return orders;
        }

        public Order_Dishes GetOrderDishByOrderId(int idOrder)
        {
            Order_Dishes orderDishes = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select TOP 1 idOrder, idDish from Orders_Dishes where idOrder = @idOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            orderDishes = new Order_Dishes();

                            orderDishes.IdOrders = (int)reader["idOrder"];

                            orderDishes.IdDishes = (int)reader["idDish"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return orderDishes;
        }

        public void DeleteOrderDish(int idOrder)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Orders_Dishes WHERE idOrder = @idOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Order_Dishes AddOrderDishes(Order_Dishes orderdishes)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Orders_Dishes(quantity, idDish, idOrder) values(@Quantity, @idDish, @idOrder); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@quantity", orderdishes.Quantity);
                    cmd.Parameters.AddWithValue("@idDish", orderdishes.IdDishes);
                    cmd.Parameters.AddWithValue("@idOrder", orderdishes.IdOrders);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return orderdishes;
        }
    }
}
