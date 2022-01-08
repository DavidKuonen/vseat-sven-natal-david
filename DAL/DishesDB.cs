using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            List<Dishes> allDishes = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dishes";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (allDishes == null)
                                allDishes = new List<Dishes>();

                            Dishes dish = new Dishes();

                            dish.IdDishes = (int)reader["idDish"];

                            if (reader["name"] != null)
                            {
                                dish.Name = (string)reader["name"];
                            }

                            if (reader["price"] != DBNull.Value)
                            {
                                dish.Price = Convert.ToSingle(reader["price"]);
                            }

                            if (reader["calories"] != DBNull.Value)
                            {
                                dish.Calories = (int)reader["calories"];
                            }

                            if (reader["idRestaurant"] != DBNull.Value)
                            {
                                dish.IdRestaurant = (int)reader["idRestaurant"];
                            }

                            if (reader["idCategoryDish"] != DBNull.Value)
                            {
                                dish.IdCategoryDishes = (int)reader["idCategoryDish"];
                            }

                            allDishes.Add(dish);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return allDishes;
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

                            dish.IdDishes = (int)reader["idDish"];

                            if (reader["name"] != null)
                            {
                                dish.Name = (string)reader["name"];
                            }

                            if (reader["price"] != DBNull.Value)
                            {
                                dish.Price = Convert.ToSingle(reader["price"]);
                            }

                            if (reader["calories"] != DBNull.Value)
                            {
                                dish.Calories = (int)reader["calories"];
                            }

                            if (reader["idRestaurant"] != DBNull.Value)
                            {
                                dish.IdRestaurant = (int)reader["idRestaurant"];
                            }

                            if (reader["idCategoryDish"] != DBNull.Value)
                            {
                                dish.IdCategoryDishes = (int)reader["idCategoryDish"];
                            }
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

        public List<Dishes> GetDishesByRestaurantId(int idRestaurant)
        {
            List<Dishes> dishesByRestaurantId = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dishes WHERE idRestaurant=@idRestaurant";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (dishesByRestaurantId == null)
                                dishesByRestaurantId = new List<Dishes>();

                            Dishes dish = new Dishes();

                            dish.IdDishes = (int)reader["idDish"];

                            if (reader["name"] != null)
                            {
                                dish.Name = (string)reader["name"];
                            }

                            if (reader["price"] != DBNull.Value)
                            {
                                dish.Price = Convert.ToSingle(reader["price"]);
                            }

                            if (reader["calories"] != DBNull.Value)
                            {
                                dish.Calories = (int)reader["calories"];
                            }

                            if (reader["idRestaurant"] != DBNull.Value)
                            {
                                dish.IdRestaurant = (int)reader["idRestaurant"];
                            }

                            if (reader["image"] != DBNull.Value)
                            {
                                dish.Image = (string)reader["image"];
                            }

                            if (reader["idCategoryDish"] != DBNull.Value)
                            {
                                dish.IdCategoryDishes = (int)reader["idCategoryDish"];
                            }

                            dishesByRestaurantId.Add(dish);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dishesByRestaurantId;
        }
    }
}
