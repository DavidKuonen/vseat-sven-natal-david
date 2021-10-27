using DTO;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class OrderStatusDB : IOrderStatusDB
    {
        private IConfiguration Configuration { get; }

        public OrderStatusDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<OrderStatus> GetAllUserRoles()
        {
            List<OrderStatus> allOrderStatus = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM OrderStatus";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (allOrderStatus == null)
                                allOrderStatus = new List<OrderStatus>();

                            OrderStatus orderStatus = new OrderStatus();

                            orderStatus.IdOrderStatus = (int)reader["idOrderStatus"];

                            orderStatus.Status = (string)reader["status"];

                            allOrderStatus.Add(orderStatus);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return allOrderStatus;
        }

        public OrderStatus GetOrderStatusById(int idOrderStatus)
        {
            OrderStatus orderStatus = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM OrderStatus WHERE idOrderStatus = @idOrderStatus";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idOrderStatus", idOrderStatus);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderStatus = new OrderStatus();

                            orderStatus.IdOrderStatus = (int)reader["idOrderStatus"];

                            orderStatus.Status = (string)reader["status"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return orderStatus;
        }
    }
}
