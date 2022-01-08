﻿using DTO;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class EmployeesDB : IEmployeesDB
    {
        private IConfiguration Configuration { get; }

        public EmployeesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> allEmployees = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Employees";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (allEmployees == null)
                                allEmployees = new List<Employee>();

                            Employee employee = new Employee();

                            employee.IdEmployee = (int)reader["idEmployee"];

                            employee.Lastname = (string)reader["lastname"];

                            employee.Firstname = (string)reader["firstname"];

                            employee.Address = (string)reader["address"];

                            if (reader["phonenumber"] != null)
                                employee.PhoneNumber = (string)reader["phonenumber"];

                            employee.Email = (string)reader["email"];

                            employee.Password = (string)reader["password"];

                            employee.OpenOrders = (int)reader["openOrders"];

                            employee.IdVillage = (int)reader["idVillage"];

                            employee.IdDistrict = (int)reader["idDistrict"];

                            employee.IdUserRole = (int)reader["idUserRole"];

                            allEmployees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return allEmployees;
        }

        public Employee GetEmployeeById(int idEmployee)
        {
            Employee employee = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Employees WHERE idEmployee = @idEmployee";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@idEmployee", idEmployee);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = new Employee();

                            employee.IdEmployee = (int)reader["idEmployee"];

                            employee.Lastname = (string)reader["lastname"];

                            employee.Firstname = (string)reader["firstname"];

                            employee.Address = (string)reader["address"];

                            if (reader["phoneNumber"] != null)
                                employee.PhoneNumber = (string)reader["phoneNumber"];

                            employee.Email = (string)reader["email"];

                            employee.Password = (string)reader["password"];

                            employee.IdVillage = (int)reader["idVillage"];

                            employee.IdDistrict = (int)reader["idDistrict"];

                            employee.IdUserRole = (int)reader["idUserRole"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employee;
        }

        public Employee GetEmployee(string Email, string Password)
        {
            Employee employee = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Employees WHERE email = @email AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@password", Password);

                    sqlConn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = new Employee();

                            employee.IdEmployee = (int)reader["idEmployee"];

                            employee.Lastname = (string)reader["lastname"];

                            employee.Firstname = (string)reader["firstname"];

                            employee.Address = (string)reader["address"];

                            employee.PhoneNumber = (string)reader["phoneNumber"];

                            employee.Email = (string)reader["email"];

                            employee.Password = (string)reader["password"];

                            employee.IdVillage = (int)reader["idVillage"];

                            employee.IdUserRole = (int)reader["idUserRole"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employee;
        }

        public void UpdateOpenOrders(int EmployeeId)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Employees " +
                      "SET openOrders = (openOrders+1)" +
                      "WHERE idEmployee = @idEmployee";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idEmployee", EmployeeId);


                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Employee AddEmployee(Employee employee)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employees (lastname, firstname, address, phoneNumber, email, password, idVillage, idUserRole)" +
                                   "VALUES (@lastname, @firstname, @address, @phoneNumber, @email, @password, @idVillage, @idUserRole)";

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.Parameters.AddWithValue("@lastname", employee.Lastname);
                    cmd.Parameters.AddWithValue("@firstname", employee.Firstname);
                    cmd.Parameters.AddWithValue("@address", employee.Address);
                    cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", employee.Email);
                    cmd.Parameters.AddWithValue("@password", employee.Password);
                    cmd.Parameters.AddWithValue("@idVillage", employee.IdVillage);
                    cmd.Parameters.AddWithValue("@idUserRole", employee.IdUserRole);

                    sqlConn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employee;
        }
    }
}
