using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DAL
{
    public class VillagesDB : IVillagesDB
    {
        private IConfiguration Configuration { get; }

        public VillagesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Villages> GetAllVillages()
        {
            List<Villages> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Villages";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Villages>();

                            Villages village = new Villages();

                            village.idVillage = (int)dr["idVillage"];

                            village.postalCode = (int)dr["postalCode"];

                            village.name = (string)dr["name"];

                            village.idDistrict = (int)dr["idDistrict"];

                            results.Add(village);
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

        public List<Villages> GetVillagesByDistrict(int idDistrict)
        {
            List<Villages> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Villages where idDistrict = @idDistrict";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDistrict", idDistrict);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Villages>();

                            Villages village = new Villages();

                            village.idVillage = (int)dr["idVillage"];

                            village.postalCode = (int)dr["postalCode"];

                            village.name = (string)dr["name"];

                            village.idDistrict = (int)dr["idDistrict"];

                            results.Add(village);
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
