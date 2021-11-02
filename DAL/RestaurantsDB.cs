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

                            Restaurant.idRestaurant = (int)dr["idRestaurant"];

                            Restaurant.name = (string)dr["name"];

                            Restaurant.address = (string)dr["address"];

                            if (dr["phoneNumber"] != null)
                                Restaurant.phoneNumber = (string)dr["phoneNumber"];

                            Restaurant.idVillage = (int)dr["idVillage"];

                            Restaurant.idDistrict = (int)dr["idDistrict"];

                            Restaurant.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

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

        public List<Restaurants> GetRestaurantsByName(string name)
        {
            List<Restaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where name = @name";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@name", name);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurants>();

                            Restaurants Restaurant = new Restaurants();

                            Restaurant.idRestaurant = (int)dr["idRestaurant"];

                            Restaurant.name = (string)dr["name"];

                            Restaurant.address = (string)dr["address"];

                            if (dr["phoneNumber"] != null)
                                Restaurant.phoneNumber = (string)dr["phoneNumber"];

                            Restaurant.idVillage = (int)dr["idVillage"];

                            Restaurant.idDistrict = (int)dr["idDistrict"];

                            Restaurant.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

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

        public List<Restaurants> GetRestaurantsByVillage(int idVillage)
        {
            List<Restaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where idVillage = @idVillage";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idVillage", idVillage);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurants>();

                            Restaurants Restaurant = new Restaurants();

                            Restaurant.idRestaurant = (int)dr["idRestaurant"];

                            Restaurant.name = (string)dr["name"];

                            Restaurant.address = (string)dr["address"];

                            if (dr["phoneNumber"] != null)
                                Restaurant.phoneNumber = (string)dr["phoneNumber"];

                            Restaurant.idVillage = (int)dr["idVillage"];

                            Restaurant.idDistrict = (int)dr["idDistrict"];

                            Restaurant.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

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

        public List<Restaurants> GetRestaurantsByDistrict(int idDistrict)
        {
            List<Restaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where idDistrict = @idDistrict";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDistrict", idDistrict);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurants>();

                            Restaurants Restaurant = new Restaurants();

                            Restaurant.idRestaurant = (int)dr["idRestaurant"];

                            Restaurant.name = (string)dr["name"];

                            Restaurant.address = (string)dr["address"];

                            if (dr["phoneNumber"] != null)
                                Restaurant.phoneNumber = (string)dr["phoneNumber"];

                            Restaurant.idVillage = (int)dr["idVillage"];

                            Restaurant.idDistrict = (int)dr["idDistrict"];

                            Restaurant.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

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

        public List<Restaurants> GetRestaurantsByCategoryRestaurant(int idCategoryRestaurant)
        {
            List<Restaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where idCategoryRestaurant = @idCategoryRestaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCategoryRestaurant", idCategoryRestaurant);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurants>();

                            Restaurants Restaurant = new Restaurants();

                            Restaurant.idRestaurant = (int)dr["idRestaurant"];

                            Restaurant.name = (string)dr["name"];

                            Restaurant.address = (string)dr["address"];

                            if (dr["phoneNumber"] != null)
                                Restaurant.phoneNumber = (string)dr["phoneNumber"];

                            Restaurant.idVillage = (int)dr["idVillage"];

                            Restaurant.idDistrict = (int)dr["idDistrict"];

                            Restaurant.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

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
                 
                    cmd.Parameters.AddWithValue("@name", restaurant.name);
                    cmd.Parameters.AddWithValue("@address", restaurant.address);
                    cmd.Parameters.AddWithValue("@phoneNumber", restaurant.phoneNumber);
                    cmd.Parameters.AddWithValue("@idVillage", restaurant.idVillage);
                    cmd.Parameters.AddWithValue("@idDistrict", restaurant.idDistrict);
                    cmd.Parameters.AddWithValue("@idCategoryRestaurant", restaurant.idCategoryRestaurant);
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
                    string query = "DELETE FROM Restaurants WHERE RestaurantID = @id";

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
