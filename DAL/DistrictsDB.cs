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

        public Districts GetDistrictsById(int id)
        {
            Districts result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //DefaultConnection wird im JSON-File definiert für Datenbankverbindung

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

                            result.idDistrict = (int)dr["idDistrict"];

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
