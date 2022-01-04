using DTO;

using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class CustomersDB : ICustomersDB
    {
        private IConfiguration Configuration { get; }

        public CustomersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> allCustomers = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (allCustomers == null)
                                allCustomers = new List<Customer>();

                            Customer customer = new Customer();

                            customer.IdCustomer = (int)reader["idCustomer"];

                            customer.Lastname = (string)reader["lastname"];

                            customer.Firstname = (string)reader["firstname"];

                            customer.Address = (string)reader["address"];

                            customer.PhoneNumber = (string)reader["phoneNumber"];

                            customer.Email = (string)reader["email"];

                            customer.Password = (string)reader["password"];

                            customer.IdVillage = (int)reader["idVillage"];

                            customer.IdUserRole = (int)reader["idUserRole"];

                            allCustomers.Add(customer);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return allCustomers;
        }

        public Customer GetCustomerById(int idCustomer)
        {
            Customer customer = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Customers where idCustomer = @idCustomer";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCustomer", idCustomer);

                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer();

                            customer.IdCustomer = (int)reader["idCustomer"];

                            customer.Lastname = (string)reader["lastname"];

                            customer.Firstname = (string)reader["firstname"];

                            customer.Address = (string)reader["address"];

                            customer.PhoneNumber = (string)reader["phoneNumber"];

                            customer.Email = (string)reader["email"];

                            customer.Password = (string)reader["password"];

                            customer.Registered = (DateTime)reader["registered"];

                            customer.IdVillage = (int)reader["idVillage"];

                            customer.IdDistrict = (int)reader["idDistrict"];

                            customer.IdUserRole = (int)reader["idUserRole"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customer;
        }

        public Customer GetCustomerByDistrict(int idDistrict)
        {
            Customer customer = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer WHERE idDistrict = @idDistrict";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idDistrict", idDistrict);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer = new Customer();

                            customer.IdCustomer = (int)reader["idCustomer"];

                            customer.Lastname = (string)reader["lastname"];

                            customer.Firstname = (string)reader["firstname"];

                            customer.Address = (string)reader["address"];

                            customer.PhoneNumber = (string)reader["phoneNumber"];

                            customer.Email = (string)reader["email"];

                            customer.Password = (string)reader["password"];

                            customer.Registered = (DateTime)reader["registered"];

                            customer.IdVillage = (int)reader["idVillage"];

                            customer.IdDistrict = (int)reader["idDistrict"];

                            customer.IdUserRole = (int)reader["idUserRole"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customer;
        }

        public Customer GetCustomer(string Email, string Password)
        {
            Customer customer = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers WHERE email = @email AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@password", Password);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer = new Customer();

                            customer.IdCustomer = (int)reader["idCustomer"];

                            customer.Lastname = (string)reader["lastname"];

                            customer.Firstname = (string)reader["firstname"];

                            customer.Address = (string)reader["address"];

                            customer.PhoneNumber = (string)reader["phoneNumber"];

                            customer.Email = (string)reader["email"];

                            customer.Password = (string)reader["password"];

                            customer.IdVillage = (int)reader["idVillage"];

                            customer.IdUserRole = (int)reader["idUserRole"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customer;
        }

        public Customer AddCustomer(Customer customer)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Customers (lastname, firstname, address, phoneNumber, email, password, registered, idVillage, idDistrict, idUserRole)" +
                                   "VALUES (@lastname, @firstname, @address, @phoneNumber, @email, @password, @registered, @idVillage, @idDistrict, @idUserRole)";

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@lastname", customer.Lastname);
                    cmd.Parameters.AddWithValue("@firstname", customer.Firstname);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@password", customer.Password);
                    cmd.Parameters.AddWithValue("@registered", customer.Registered);
                    cmd.Parameters.AddWithValue("@idVillage", customer.IdVillage);
                    cmd.Parameters.AddWithValue("@idDistrict", customer.IdDistrict);
                    cmd.Parameters.AddWithValue("@idUserRole", customer.IdUserRole);

                    sqlConn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customer;
        }



    }
}
