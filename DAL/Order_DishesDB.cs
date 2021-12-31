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

              order_dish.idOrder_Dishes = (int)dr["idOrder_Dish"];

              if (dr["quantity"] != null)
                order_dish.Quantity = (int)dr["quantity"];

              if (dr["idDish"] != null)
                order_dish.FK_Dishes = (int)dr["idDish"];

              if (dr["idOrder"] != DBNull.Value)
                order_dish.FK_Orders = (int)dr["idOrder"];

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

    public List<Order_Dishes> GetOrderDishesByOrderId(int id)
    {
      List<Order_Dishes> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Orders_Dishes WHERE idOrder=@id";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@id", id);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

             if (results == null)
                results = new List<Order_Dishes>();

              Order_Dishes result = new Order_Dishes();

              result.idOrder_Dishes = (int)dr["idOrder_Dish"];

              result.Quantity = (int)dr["quantity"];

              result.FK_Dishes = (int)dr["idDish"];

              result.FK_Orders = (int)dr["idOrder"];

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

    public Order_Dishes GetOrderDishesById(int id)
    {
      Order_Dishes result = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Order_Dishes WHERE idOrder_Dish=@id";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@id", id);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              result = new Order_Dishes();

              result.idOrder_Dishes = (int)dr["idOrder_Dish"];

              result.Quantity = (int)dr["quantity"];

              result.FK_Dishes = (int)dr["idDish"];

              result.FK_Orders = (int)dr["idOrder"];

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
                    string query = "Insert into Orders_Dishes(quantity, idDish, idOrder) values(@Quantity, @idDish, @idOrder); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@quantity", orderdishes.Quantity);
                    cmd.Parameters.AddWithValue("@idDish", orderdishes.FK_Dishes);
                    cmd.Parameters.AddWithValue("@idOrder", orderdishes.FK_Orders);

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
