using DTO;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class UserRolesDB : IUserRolesDB
    {
        private IConfiguration Configuration { get; }

        public UserRolesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<UserRole> GetAllUserRoles()
        {
            List<UserRole> allUserRoles = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM UserRoles";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (allUserRoles == null)
                                allUserRoles = new List<UserRole>();

                            UserRole userRole = new UserRole();

                            userRole.IdUserRole = (int)reader["idUserRole"];

                            userRole.Type = (string)reader["type"];

                            allUserRoles.Add(userRole);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return allUserRoles;
        }

        public UserRole GetEmployeeById(int idUserRole)
        {
            UserRole userRole = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM UserRoles WHERE idUserRole = @idUserRole";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idUserRole", idUserRole);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userRole = new UserRole();

                            userRole.IdUserRole = (int)reader["idUserRole"];

                            userRole.Type = (string)reader["type"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return userRole;
        }
    }
}
