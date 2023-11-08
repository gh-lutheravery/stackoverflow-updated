using System.Data.SqlClient;

namespace WebApplication5.Data
{
    public class SqlServerConnection
    {
        public SqlConnection GetDbConnection()
        {
            string connectionString = "server=LAPTOP-E3LRK6RR\\SQLEXPRESS01;database=webapp5db;integrated Security=SSPI;";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }
    }
}
