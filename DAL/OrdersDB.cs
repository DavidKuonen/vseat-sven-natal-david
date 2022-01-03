using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
 public class OrdersDB : IOrdersDB
  {
    private IConfiguration Configuration { get; }

    public OrdersDB(IConfiguration configuration)
    {
      Configuration = configuration;

    }


    public List<Orders> GetAllOrders()
    {
      List<Orders> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders";
          SqlCommand cmd = new SqlCommand(query, cn);
          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              if (results == null)
                results = new List<Orders>();

              Orders order = new Orders();

              order.idOrders = (int)dr["idOrder"];

              if (dr["orderTime"] != null)
                order.OrderTime = (DateTime)dr["orderTime"];

              if (dr["deliveryTime"] != null)
                order.DeliveryTime = (DateTime)dr["deliveryTime"];

              if (dr["totalPrice"] != DBNull.Value)
                order.TotalPrice = (float)dr["totalPrice"];

              if (dr["idCustomer"] != DBNull.Value)
                order.FK_Customers = (int)dr["idCustomer"];

              if (dr["idOrderStatus"] != DBNull.Value)
                order.FK_OrderStatus = (int)dr["idOrderStatus"];

              if (dr["idEmployee"] != DBNull.Value)
                order.FK_Staff = (int)dr["idEmployee"];

              results.Add(order);

            }
          }
        }
      }
      catch (Exception)
      {
        throw;
      }

      return results;
    }

        public List<Orders> GetOrdersByStaffId(int idEmployee)
        {
            List<Orders> orders = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE idEmployee = @idEmployee";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idEmployee", idEmployee);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (orders == null)
                                orders = new List<Orders>();

                            Orders order = new Orders();

                            order.idOrders = (int)reader["idOrder"];

                            order.OrderTime = (DateTime)reader["orderTime"];

                            order.DeliveryTime = (DateTime)reader["deliveryTime"];

                            order.TotalPrice = (float)reader["totalPrice"];

                            order.FK_Customers = (int)reader["idCustomer"];

                            order.FK_Staff = (int)reader["idEmployee"];

                            order.FK_OrderStatus = (int)reader["idOrderStatus"];

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
                    string query = "SELECT * FROM Orders WHERE idEmployee = @idEmployee AND idOrderStatus = @idOrderStatus";
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

                            order.idOrders = (int)reader["idOrder"];

                            order.OrderTime = (DateTime)reader["orderTime"];

                            order.DeliveryTime = (DateTime)reader["deliveryTime"];

                            order.TotalPrice = Convert.ToSingle(reader["totalPrice"]);

                            order.FK_Customers = (int)reader["idCustomer"];

                            order.FK_Staff = (int)reader["idEmployee"];

                            order.FK_OrderStatus = (int)reader["idOrderStatus"];

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

        public List<Orders> GetOrdersByStaffIdAndCustomerId(int idStaff, int idEmployee)
    {
      List<Orders> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders WHERE idEmployee = @idStaff AND idCustomer = @idCustomer";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@idStaff", idStaff);
          cmd.Parameters.AddWithValue("@idCustomer", idEmployee);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              if (results == null)
                results = new List<Orders>();

              Orders result = new Orders();

              result.idOrders = (int)dr["idOrder"];

              result.OrderTime = (DateTime)dr["orderTime"];

              result.DeliveryTime = (DateTime)dr["deliveryTime"];

              result.TotalPrice = (float)dr["totalPrice"];

              result.FK_Customers = (int)dr["idCustomer"];

              result.FK_Staff = (int)dr["idEmployee"];

              result.FK_OrderStatus = (int)dr["idOrderStatus"];

              results.Add(result);

            }
          }
        }
      }
      catch (Exception)
      {
        throw;
      }

      return results;
    }

    public List<Orders> GetOrdersByCustomerId(int id)
    {
      List<Orders> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders WHERE idCustomer=@id";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@id", id);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              Orders result = new Orders();

              result.idOrders = (int)dr["idOrder"];

              result.OrderTime = (DateTime)dr["orderTime"];

              result.DeliveryTime = (DateTime)dr["deliveryTime"];

              result.TotalPrice = (float)dr["totalPrice"];

              result.FK_Customers = (int)dr["idCustomer"];

              result.FK_Staff = (int)dr["idEmployee"];

              result.FK_OrderStatus = (int)dr["idOrderStatus"];

              results.Add(result);
            }
          }
        }
      }
      catch (Exception)
      {
        throw;
      }

      return results;
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

                            result.idOrders = (int)reader["idOrder"];

                            result.OrderTime = (DateTime)reader["orderTime"];

                            result.DeliveryTime = (DateTime)reader["deliveryTime"];

                            result.TotalPrice = Convert.ToSingle(reader["totalPrice"]);

                            result.FK_Customers = (int)reader["idCustomer"];

                            result.FK_Staff = (int)reader["idEmployee"];

                            result.FK_OrderStatus = (int)reader["idOrderStatus"];

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

        public int GetOrdersNotDelivered(int idEmployee, DateTime deliveryTime)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int count;
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                { 
                    string query = "SELECT COUNT(*) FROM Orders WHERE idEmployee = @idEmployee AND idOrderStatus = idOrderStatus AND deliveryTime BETWEEN @deliveryTime AND DATEADD(MI, 30, @deliveryTime);";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@idEmployee", idEmployee);
                        cmd.Parameters.AddWithValue("@idOrderStatus", 1);
                        cmd.Parameters.AddWithValue("@deliveryTime", deliveryTime);
                        sqlConn.Open();
                        count = Convert.ToInt32(cmd.ExecuteScalar());

                    };
                }
            }
            catch (Exception)
            {
                throw;
            }

            return count;
        }



        public int GetOrderIdByCustomerId(int idCustomer, int idEmployee)
    {
      int result = 0;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders WHERE idCustomer = @idCustomer AND idEmployee = @idEmployee";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@idCustomer", idCustomer);
          cmd.Parameters.AddWithValue("@idEmployee", idEmployee);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              result= (int)dr["idOrder"];

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

        public void UpdateOrderPrice(int orderId,float price)
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
                    cmd.Parameters.AddWithValue("@idOrderStatus", order.FK_OrderStatus);
                    cmd.Parameters.AddWithValue("@idEmployee", order.FK_Staff);
                    cmd.Parameters.AddWithValue("@idCustomer", order.FK_Customers);

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
