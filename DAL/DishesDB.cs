﻿using DTO;
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


  }
}
