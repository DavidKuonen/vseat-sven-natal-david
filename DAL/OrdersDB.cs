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

              order.idOrders = (int)dr["idOrders"];

              if (dr["OrderTime"] != null)
                order.OrderTime = (DateTime)dr["OrderTime"];

              if (dr["DeliveryTime"] != null)
                order.DeliveryTime = (DateTime)dr["DeliveryTime"];

              if (dr["TotalPrice"] != DBNull.Value)
                order.TotalPrice = (float)dr["TotalPrice"];

              if (dr["FK_Customers"] != DBNull.Value)
                order.FK_Customers = (int)dr["FK_Customers"];

              if (dr["FK_OrderStatus"] != DBNull.Value)
                order.FK_OrderStatus = (int)dr["FK_OrderStatus"];

              if (dr["FK_Staff"] != DBNull.Value)
                order.FK_Staff = (int)dr["FK_Staff"];

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

              result.idOrders = (int)dr["idOrders"];

              result.OrderTime = (DateTime)dr["OrderTime"];

              result.DeliveryTime = (DateTime)dr["DeliveryTime"];

              result.TotalPrice = (float)dr["TotalPrice"];

              result.FK_Customers = (int)dr["FK_Customers"];

              result.FK_Staff = (int)dr["FK_Staff"];

              result.FK_OrderStatus = (int)dr["FK_OrderStatus"];


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


    public Orders AddOrder(Orders order)
    {
      int result = 0;

      string connectionString = Configuration.GetConnectionString("DefaultConnection");

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Insert into Orders(idOrders,OrderTime, DeliveryTime, [Total Price], FK_Customers, FK_Staff, FK_OrderStatus) " +
            "values(@idOrders,@OrderTime, @DeliveryTime, @totalprice, @FK_Customers, @FK_Staff, @FK_OrderStatus)";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@idOrders", order.idOrders);
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
