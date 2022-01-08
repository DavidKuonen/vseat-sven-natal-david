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

                            village.IdVillage = (int)dr["idVillage"];

                            village.PostalCode = (int)dr["postalCode"];

                            village.Name = (string)dr["name"];

                            village.IdDistrict = (int)dr["idDistrict"];

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

        public Villages GetVillageByName(string Village)
        {
            Villages village = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Villages where name = @village";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@village", Village);

                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            village = new Villages();

                            village.IdVillage = (int)reader["idVillage"];

                            village.PostalCode = (int)reader["postalCode"];

                            village.Name = (string)reader["name"];

                            village.IdDistrict = (int)reader["idDistrict"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return village;
        }

        public Villages GetVillageById(int idVillage)
        {
            Villages village = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Villages where idVillage = @idVillage";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idVillage", idVillage);

                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            village = new Villages();

                            village.IdVillage = (int)reader["idVillage"];

                            village.PostalCode = (int)reader["postalCode"];

                            village.Name = (string)reader["name"];

                            village.IdDistrict = (int)reader["idDistrict"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return village;
        }
    }
}
