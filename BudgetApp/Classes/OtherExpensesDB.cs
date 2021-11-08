using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Classes
{
    class OtherExpensesDB
    {
        public static void AddOtherExpenses(string name, IDictionary<string, decimal> otherExp)
        {
            int count = otherExp.Count;
            var exp = new List<string>();
            var cost = new List<decimal>();
            foreach (KeyValuePair<string,decimal> item in otherExp)
            {
                exp.Add(item.Key);
                cost.Add(item.Value);
            }

            int x = 0;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        while (count > 0)
                        {

                            var command = new SqlCommand("INSERT INTO OTHER_EXPENSES(FK_USERNAME,OTHER_EXPENSE,COST) " +
                                "VALUES(@USERNAME,@OTHER_EXPENSE,@COST)", connection);

                            command.Parameters.AddWithValue("@USERNAME", name);
                            command.Parameters.AddWithValue("@OTHER_EXPENSE",exp[x]);
                            command.Parameters.AddWithValue("@COST",cost[x]);

                            command.ExecuteNonQuery();
                            x++;
                            count = count - 1;
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
            
        }

        public static bool FindEntry(string username)
        {
            bool found = false;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT FK_USERNAME FROM OTHER_EXPENSES WHERE (FK_USERNAME = @username)", connection);

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
        public static Dictionary<string, decimal> GetOtherList(string username)
        {
            var otherList = new Dictionary<string,decimal>();
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT OTHER_EXPENSE,COST FROM OTHER_EXPENSES WHERE (FK_USERNAME = @username)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                otherList.Add(reader["OTHER_EXPENSE"].ToString(),Convert.ToDecimal(reader["COST"]));
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
            return otherList;
        }
        public static List<decimal> GetCostList(string username)
        {
            var costList = new List<decimal>();
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT COST FROM OTHER_EXPENSES WHERE (FK_USERNAME = @username)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                costList.Add(Convert.ToDecimal(reader["COST"]));
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
            return costList;
        }
    }
}
