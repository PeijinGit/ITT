using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class User:BaseDAL, IUserDAL
    {

        public User(IOptions<AppSettingModels> appSettings) : base(appSettings)
        {
        }

        public int ThirdPartyLogin(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ThirdPartyLogin", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@username", username));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            return -1;
                        }
                        else
                        {
                            int userId = -1;
                            while (reader.Read())
                            {
                                userId = (int)reader["Id"];
                            }
                            return userId;
                        }
                    }
                }
            }
        }

        public int UserRegister(string username, string password, int usertype)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("UserRegister", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@username", username));
                    command.Parameters.Add(new SqlParameter("@pwd", password));
                    command.Parameters.Add(new SqlParameter("@atype", usertype));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            return -1;
                        }
                        else
                        {
                            int counter = 0;
                            while (reader.Read())
                            {
                                counter++;
                            }
                            return counter;
                        }
                    }
                }
            }
        }

        public int ValidateLogin(string username,string pwd) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ValidateLogin", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@username", username));
                    command.Parameters.Add(new SqlParameter("@pwd", pwd));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            return -1;
                        }
                        else 
                        {
                            int userId = -1;
                            while (reader.Read()) 
                            {
                                userId = (int)reader["Id"];
                            }
                            return userId;
                        }
                    }
                }
            }
        }

        
    }
}
