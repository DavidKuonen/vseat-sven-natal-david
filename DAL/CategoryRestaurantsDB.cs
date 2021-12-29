using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DAL
{
    public class CategoryRestaurantsDB : ICategoryRestaurantsDB
    {
        private IConfiguration Configuration { get; }

        public CategoryRestaurantsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<CategoryRestaurants> GetAllCategoryRestaurants()
        {
            List<CategoryRestaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from CategoryRestaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<CategoryRestaurants>();

                            CategoryRestaurants categoryRestaurant = new CategoryRestaurants();

                            categoryRestaurant.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

                            categoryRestaurant.name = (string)dr["name"];

                            results.Add(categoryRestaurant);
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

        public CategoryRestaurants GetCategoryRestaurantsById(int id)
        {
            CategoryRestaurants result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from CategoryRestaurants WHERE idCategoryRestaurant=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            result = new CategoryRestaurants();

                            result.idCategoryRestaurant = (int)dr["idCategoryRestaurant"];

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
