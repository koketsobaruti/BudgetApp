using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Classes
{
    class RentalDB
    {
        /// <summary>
        /// add a new instance of a rental
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public static void AddRental(string name, decimal amount)
        {
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand("INSERT INTO TBL_RENTAL(FK_USERNAME,RENTAL_AMOUNT)" +
                            " VALUES(@FK_USERNAME,@RENTAL_AMOUNT)", connection);

                        command.Parameters.AddWithValue("@FK_USERNAME", name);
                        command.Parameters.AddWithValue("@RENTAL_AMOUNT", amount);

                        command.ExecuteNonQuery();
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
        }
        /// <summary>
        /// find out if a particular user is renting or not
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool FindRentalUser(string username)
        {
            bool found = false;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT COUNT(*) FROM TBL_RENTAL WHERE (FK_USERNAME = @username)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static decimal GetRentalAmount(string username)
        {
            decimal cost=0;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT RENTAL_AMOUNT FROM TBL_RENTAL WHERE (FK_USERNAME = @username)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cost = Convert.ToDecimal(reader["RENTAL_AMOUNT"]);
                            }
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
            return cost;
        }
    }
}
