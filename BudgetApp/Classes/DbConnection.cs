using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Classes
{
    class DbConnection : DbContext 
    {
        public static SqlConnection GetDbConnection()
        {
            var connString = "Data Source=LAPTOP-FO39IEVO\\SQLEXPRESS;Initial Catalog=Personal_Planner_Database;Integrated Security=True";
            var connection = new SqlConnection(connString);
            return connection;
        }
    }
}
