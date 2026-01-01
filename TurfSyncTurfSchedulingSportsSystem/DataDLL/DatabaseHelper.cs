using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.DataDLL
{
    internal class DatabaseHelper
    {
        private static string connectionString = "Data Source=FAIYAZERIN\\SQLEXPRESS;Initial Catalog=TurfSyncDB;User ID=fairuzfaiyaz;Password=123;";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
