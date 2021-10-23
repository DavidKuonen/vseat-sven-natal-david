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
  class CategoryDishesDB : ICategoryDishesDB
  {
    private IConfiguration Configuration { get; }

    public CategoryDishesDB(IConfiguration configuration)
    {
      Configuration = configuration;

    }

    public List<CategoryDishes> GetAllCategoryDishes()
    {
      List<CategoryDishes> results = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from CategoryDishes";
          SqlCommand cmd = new SqlCommand(query, cn);
          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              if (results == null)
                results = new List<CategoryDishes>();

              CategoryDishes categorydish = new CategoryDishes();

              categorydish.idCategorie = (int)dr["idCategorie"];

              if (dr["name"] != null)
                categorydish.name = (string)dr["name"];

              results.Add(categorydish);

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

    public CategoryDishes GetCategoryDishesByName(string name)
    {
      CategoryDishes result = null;
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

      try
      {
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
          string query = "Select * from CategoryDishes WHERE name=@name";
          SqlCommand cmd = new SqlCommand(query, cn);
          cmd.Parameters.AddWithValue("@name", name);

          cn.Open();

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            if (dr.Read())
            {

              result = new CategoryDishes();

              result.idCategorie = (int)dr["idCategorie"];

              result.name = (string)dr["name"];

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




  }
}
