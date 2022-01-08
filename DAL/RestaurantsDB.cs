using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class RestaurantsDB : IRestaurantsDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Restaurants> GetAllRestaurants()
        {
            List<Restaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurants>();

                            Restaurants Restaurant = new Restaurants();

                            Restaurant.IdRestaurant = (int)dr["idRestaurant"];

                            Restaurant.Name = (string)dr["name"];

                            Restaurant.Address = (string)dr["address"];

                            if (dr["phoneNumber"] != null)
                                Restaurant.PhoneNumber = (string)dr["phoneNumber"];

                            Restaurant.IdVillage = (int)dr["idVillage"];

                            Restaurant.IdDistrict = (int)dr["idDistrict"];

                            Restaurant.IdCategoryRestaurant = (int)dr["idCategoryRestaurant"];

                            Restaurant.RestaurantImage = (string)dr["image"];

                            results.Add(Restaurant);
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

        public Restaurants GetRestaurantById(int idRestaurant)
        {
            Restaurants restaurant = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where idRestaurant = @idRestaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);

                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            restaurant = new Restaurants();

                            restaurant.IdRestaurant = (int)reader["idRestaurant"];

                            if (reader["name"] != null)
                                restaurant.Name = (string)reader["name"];

                            if (reader["address"] != null)
                                restaurant.Address = (string)reader["address"];

                            if (reader["phoneNumber"] != null)
                                restaurant.PhoneNumber = (string)reader["phoneNumber"];

                            restaurant.IdVillage = (int)reader["idVillage"];

                            restaurant.IdDistrict = (int)reader["idDistrict"];

                            restaurant.IdCategoryRestaurant = (int)reader["idCategoryRestaurant"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return restaurant;
        }

        public Restaurants AddRestaurant(Restaurants restaurant)
        {
            int results = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Restaurants(name, address, phoneNumber, idVillage, idDistrict, idCategoryRestaurant)" +
                                   "values (@name, @address, @phoneNumber, @idVillage, @idDistrict, @idCategoryRestaurant) ";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@address", restaurant.Address);
                    cmd.Parameters.AddWithValue("@phoneNumber", restaurant.PhoneNumber);
                    cmd.Parameters.AddWithValue("@idVillage", restaurant.IdVillage);
                    cmd.Parameters.AddWithValue("@idDistrict", restaurant.IdDistrict);
                    cmd.Parameters.AddWithValue("@idCategoryRestaurant", restaurant.IdCategoryRestaurant);
                    cn.Open();

                    results = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                throw;
            }

            return restaurant;
        }

        public int DeleteRestaurant(int id)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Restaurants WHERE idRestaurant = @id";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();

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
