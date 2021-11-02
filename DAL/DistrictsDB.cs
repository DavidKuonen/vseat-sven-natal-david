using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public List<Districts> GetAllDistricts()
        {
            List<Districts> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Districts";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Districts>();

                            Districts district = new Districts();

                            district.idDistrict = (int)dr["idDistrict"];

                            district.name = (string)dr["name"];

                            results.Add(district);
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
    }
}
