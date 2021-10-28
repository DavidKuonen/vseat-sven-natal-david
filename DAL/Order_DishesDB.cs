using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class Order_DishesDB : IOrder_DishesDB
  {
    private IConfiguration Configuration { get; }

    public Order_DishesDB(IConfiguration configuration)
    {
      Configuration = configuration;

    }


    public List<Order_Dishes> GetAllOrder_Dishes()
    {
      List<Order_Dishes> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Order_Dishes";
          SqlCommand cmd = new SqlCommand(query, cn);
          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              if (results == null)
                results = new List<Order_Dishes>();

              Order_Dishes order_dish = new Order_Dishes();

              order_dish.idOrder_Dishes = (int)dr["idOrder_Dishes"];

              if (dr["Quantity"] != null)
                order_dish.Quantity = (int)dr["Quantity"];

              if (dr["FK_Dishes"] != null)
                order_dish.FK_Dishes = (int)dr["FK_Dishes"];

              if (dr["FK_Orders"] != DBNull.Value)
                order_dish.FK_Orders = (int)dr["FK_Orders"];

              results.Add(order_dish);

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

    public Order_Dishes GetOrderDishesById(int id)
    {
      Order_Dishes result = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Order_Dishes WHERE idOrder_Dishes=@id";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@id", id);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              result = new Order_Dishes();

              result.idOrder_Dishes = (int)dr["idOrder_Dishes"];

              result.Quantity = (int)dr["Quantity"];

              result.FK_Dishes = (int)dr["FK_Dishes"];

              result.FK_Orders = (int)dr["FK_Orders"];

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

    public Order_Dishes AddOrderDishes(Order_Dishes orderdishes)
    {
      int result = 0;

      string connectionString = Configuration.GetConnectionString("DefaultConnection");

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Insert into Order_Dishes(idOrder_Dishes, Quantity, FK_Dishes, FK_Orders) " +
            "values(@idOrder_Dishes, @Quantity, @FK_Dishes, @FK_Orders)";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@idOrder_Dishes", orderdishes.idOrder_Dishes);
          cmd.Parameters.AddWithValue("@Quantity", orderdishes.Quantity);
          cmd.Parameters.AddWithValue("@FK_Dishes", orderdishes.FK_Dishes);
          cmd.Parameters.AddWithValue("@FK_Orders", orderdishes.FK_Orders);


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
