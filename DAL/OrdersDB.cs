using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class OrdersDB : IOrdersDB
    {
        private IConfiguration Configuration { get; }

        public OrdersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Orders> GetOpenOrdersCustomer(int idCustomer)
        {
            List<Orders> orders = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE idCustomer = @idCustomer AND idOrderStatus = @idOrderStatus ORDER BY idOrder DESC";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idCustomer", idCustomer);
                    cmd.Parameters.AddWithValue("@idOrderStatus", 1);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (orders == null)
                                orders = new List<Orders>();

                            Orders order = new Orders();

                            order.IdOrders = (int)reader["idOrder"];

                            order.OrderTime = (DateTime)reader["orderTime"];

                            order.DeliveryTime = (DateTime)reader["deliveryTime"];

                            order.TotalPrice = Convert.ToSingle(reader["totalPrice"]);

                            order.IdCustomers = (int)reader["idCustomer"];

                            order.IdEmployee = (int)reader["idEmployee"];

                            order.IdOrderStatus = (int)reader["idOrderStatus"];

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

        public List<Orders> GetOpenOrdersEmployee(int idEmployee)
        {
            List<Orders> orders = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE idEmployee = @idEmployee AND idOrderStatus = @idOrderStatus ORDER BY idOrder DESC";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idEmployee", idEmployee);
                    cmd.Parameters.AddWithValue("@idOrderStatus", 1);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (orders == null)
                                orders = new List<Orders>();

                            Orders order = new Orders();

                            order.IdOrders = (int)reader["idOrder"];

                            order.OrderTime = (DateTime)reader["orderTime"];

                            order.DeliveryTime = (DateTime)reader["deliveryTime"];

                            order.TotalPrice = Convert.ToSingle(reader["totalPrice"]);

                            order.IdCustomers = (int)reader["idCustomer"];

                            order.IdEmployee = (int)reader["idEmployee"];

                            order.IdOrderStatus = (int)reader["idOrderStatus"];

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

        public Orders GetOrderById(int orderId)
        {
            Orders result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE idOrder = @orderId";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            result = new Orders();

                            result.IdOrders = (int)reader["idOrder"];

                            result.OrderTime = (DateTime)reader["orderTime"];

                            result.DeliveryTime = (DateTime)reader["deliveryTime"];

                            result.TotalPrice = Convert.ToSingle(reader["totalPrice"]);

                            result.IdCustomers = (int)reader["idCustomer"];

                            result.IdEmployee = (int)reader["idEmployee"];

                            result.IdOrderStatus = (int)reader["idOrderStatus"];

                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int GetLastID()
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select TOP 1 idOrder from Orders ORDER BY idOrder DESC";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            result = (int)dr["idOrder"];

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public void UpdateOrderPrice(int orderId, float price)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Orders " +
                      "SET totalPrice = (totalPrice+@totalPrice)" +
                      "WHERE idOrder = @idOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@totalPrice", price);
                    cmd.Parameters.AddWithValue("@idOrder", orderId);


                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateOrderStatus(int orderId)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Orders " +
                      "SET idOrderStatus = 2" +
                      "WHERE idOrder = @idOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idOrder", orderId);


                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteOrder(int idOrder)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Orders WHERE idOrder = @idOrder";
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

        public Orders AddOrder(Orders order)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Orders(orderTime, deliveryTime, totalPrice, idOrderStatus, idEmployee, idCustomer) values (@orderTime, @deliveryTime, @totalPrice, @idOrderStatus, @idEmployee, @idCustomer); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@orderTime", order.OrderTime);
                    cmd.Parameters.AddWithValue("@deliveryTime", order.DeliveryTime);
                    cmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@idOrderStatus", order.IdOrderStatus);
                    cmd.Parameters.AddWithValue("@idEmployee", order.IdEmployee);
                    cmd.Parameters.AddWithValue("@idCustomer", order.IdCustomers);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return order;
        }
    }
}
