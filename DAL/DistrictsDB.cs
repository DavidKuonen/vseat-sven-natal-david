using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class DistrictsDB : IDistrictsDB
    {
        private IConfiguration Configuration { get; }

        public DistrictsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Districts GetDistrictsById(int id)
        {
            Districts result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Districts WHERE idDistrict=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = new Districts();

                            result.IdDistrict = (int)dr["idDistrict"];

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
