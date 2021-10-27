﻿using DTO;

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

        public Customer GetCustomerByVillage(int idVillage)
        {
            Customer customer = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer WHERE idVillage = @idVillage";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idVillage", idVillage);

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

        public Customer GetCustomer(string email, string password)
        {
            Customer customer = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers WHERE email = @email AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

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
                    string query = "INSERT INTO Customers (lastname, firstname, address, phoneNumber, email, password, registered, idVillage, idUserRole)" +
                                   "VALUES (@lastname, @firstname, @address, @phoneNumber, @email, @password, @registered, @idVillage, @idUserRole)";

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@lastname", customer.Lastname);
                    cmd.Parameters.AddWithValue("@firstname", customer.Firstname);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@password", customer.Password);
                    cmd.Parameters.AddWithValue("@registered", DateTime.Now);
                    cmd.Parameters.AddWithValue("@idVillage", customer.IdVillage);
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
