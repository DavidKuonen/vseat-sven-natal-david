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

    public List<Orders> GetOrdersByStaffId(int id)
    {
      List<Orders> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders WHERE idEmployee = @id";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@id", id);

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

              result.TotalPrice = (Double)dr["totalPrice"];

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


    public Orders GetOrdersById(int id)
    {
      Orders result = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders WHERE idOrders=@id";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@id", id);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              result = new Orders();

              result.idOrders = (int)dr["idOrder"];

              result.OrderTime = (DateTime)dr["orderTime"];

              result.DeliveryTime = (DateTime)dr["deliveryTime"];

              result.TotalPrice = (float)dr["totalPrice"];

              result.FK_Customers = (int)dr["idCustomer"];

              result.FK_Staff = (int)dr["idEmployee"];

              result.FK_OrderStatus = (int)dr["idOrderStatus"];


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

    public void UpdateOrderPrice(Orders order,float price)
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
          cmd.Parameters.AddWithValue("@idOrder", order.idOrders);


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
          string query = "Insert into Orders(orderTime, deliveryTime, totalPrice, idCustomer, idEmployee, idOrderStatus) " +
            "values(@OrderTime, @DeliveryTime, @totalprice, @FK_Customers, @FK_Staff, @FK_OrderStatus)";
          SqlCommand cmd = new SqlCommand(query, cn);
          
          cmd.Parameters.AddWithValue("@OrderTime", order.OrderTime);
          cmd.Parameters.AddWithValue("@DeliveryTime", order.DeliveryTime);
          cmd.Parameters.AddWithValue("@totalprice", order.TotalPrice);
          cmd.Parameters.AddWithValue("@FK_Customers", order.FK_Customers);
          cmd.Parameters.AddWithValue("@FK_Staff", order.FK_Staff);
          cmd.Parameters.AddWithValue("@FK_OrderStatus", order.FK_OrderStatus);

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
