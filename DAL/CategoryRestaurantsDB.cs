using DTO;
using Microsoft.Extensions.Configuration;
using System;
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

                            result.IdCategoryRestaurant = (int)dr["idCategoryRestaurant"];

                            result.Name = (string)dr["name"];
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
