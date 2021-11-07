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
  public class DishesDB : IDishesDB
  {
    private IConfiguration Configuration { get; }

    public DishesDB(IConfiguration configuration)
    {
      Configuration = configuration;

    }

    public List<Dishes> GetAllDishes()
    {
      List<Dishes> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Dishes";
          SqlCommand cmd = new SqlCommand(query, cn);
          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              if (results == null)
                results = new List<Dishes>();

              Dishes dish = new Dishes();

              dish.idDishes = (int)dr["idDish"];

              if (dr["name"] != null)
                dish.name = (string)dr["name"];

              if (dr["price"] != null)
                dish.price = (float)dr["price"];

              if (dr["calories"] != DBNull.Value)
                dish.calories = (int)dr["calories"];

              if (dr["image"] != DBNull.Value)
                dish.Image = (string)dr["image"];

              if (dr["idCategoryDish"] != DBNull.Value)
                dish.FK_CategoryDishes = (int)dr["idCategoryDish"];

              if (dr["idRestaurant"] != DBNull.Value)
                dish.FK_Restaurant = (int)dr["idRestaurant"];

              results.Add(dish);

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

        public Dishes GetDishesById(int idDish)
        {
            Dishes dish = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Dishes where idDish = @idDish";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDish", idDish);

                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dish = new Dishes();

                            dish.idDishes = (int)reader["idDish"];

                            dish.name = (string)reader["name"];

                            //dish.price = (float)reader["price"];

                            //dish.calories = (int)reader["calories"];

                            //dish.Image = (string)reader["image"];

                            dish.FK_CategoryDishes = (int)reader["idCategoryDish"];

                            dish.FK_Restaurant = (int)reader["idRestaurant"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dish;
        }

        public Dishes GetDishesByName(string name)
    {
      Dishes result = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from Dishes WHERE name=@name";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@name", name);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              result = new Dishes();

              result.idDishes = (int)dr["idDish"];

              result.name = (string)dr["name"];

              result.price = (float)dr["price"];

              result.calories = (int)dr["calories"];

              result.Image = (string)dr["image"];

              result.FK_CategoryDishes = (int)dr["FidCategoryDish"];

              result.FK_Restaurant = (int)dr["idRestaurant"];

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

    public Dishes AddDish(Dishes dish)
    {
      int result = 0;

      string connectionString = Configuration.GetConnectionString("DefaultConnection");

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Insert into Dish(name,price,calories,image,idRestaurant,idCategoryDish) " +
            "values(@name, @price, @calories, @image, @idRestaurant, @idCategoryDish)";
          SqlCommand cmd = new SqlCommand(query, cn);

          cmd.Parameters.AddWithValue("@name", dish.name);
          cmd.Parameters.AddWithValue("@price", dish.price);
          cmd.Parameters.AddWithValue("@calories", dish.calories);
          cmd.Parameters.AddWithValue("@image", dish.Image);
          cmd.Parameters.AddWithValue("@idRestaurant", dish.FK_Restaurant);
          cmd.Parameters.AddWithValue("@idCategoryDish", dish.FK_CategoryDishes);

          cn.Open();

          result = cmd.ExecuteNonQuery();
        }
      }
      catch (Exception)
      {
        throw;
      }
      return dish;
    }

  

}
}
