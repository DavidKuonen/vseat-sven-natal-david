using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class CategoryDishesDB : ICategoryDishesDB
    {
        private IConfiguration Configuration { get; }

        public CategoryDishesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public CategoryDishes GetCategoryById(int id)
        {
            CategoryDishes result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM CategoryDishes WHERE idCategoryDish=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = new CategoryDishes();

                            result.IdCategorie = (int)dr["idCategoryDish"];

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