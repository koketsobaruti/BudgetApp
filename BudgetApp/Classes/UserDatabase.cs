using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BudgetApp.Classes
{
    class UserDatabase:DbContext 
    {
        public static void AddUser(UserClass user)
        {
            SqlConnection connection = DbConnection.GetDbConnection();
            
            using (connection)
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand("INSERT INTO TBL_USER(USERNAME,FIRST_NAME, SECOND_NAME, SAVED_PASSWORD)" +
                            " VALUES (@USERNAME,@FIRST_NAME, @SECOND_NAME, @SAVED_PASSWORD)", connection);

                        command.Parameters.AddWithValue("@USERNAME",user.Username);
                        command.Parameters.AddWithValue("@FIRST_NAME",user.Name);
                        command.Parameters.AddWithValue("@SECOND_NAME",user.Surname);
                        command.Parameters.AddWithValue("@SAVED_PASSWORD",user.Password);

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }

                }
                catch(Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static bool FindAccount(string username, string password)
        {
            bool found = false;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT USERNAME,FIRST_NAME, SECOND_NAME FROM TBL_USER WHERE (USERNAME = @username) AND (SAVED_PASSWORD = @password)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            found = true;
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }


            }
            return found;
        }

        public static bool GetUser(string username)
        {
            bool found = false;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT USERNAME FROM TBL_USER WHERE (USERNAME = @username) AND (SAVED_PASSWORD = @password)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            found = true;
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }


            }
            return found;
        }
        public static bool FindUsername(string username)
        {
            bool found = false;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT USERNAME FROM TBL_USER WHERE (USERNAME = @username)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            found = true;
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }


            }
            return found;
        }
        //public static UserClass GetUserAccount
    }
}
