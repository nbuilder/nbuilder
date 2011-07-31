using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace NBuilder.Website.Tests
{
    public static class DatabaseHelper
    {
        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                _connectionString = _connectionString ?? ConfigurationManager.AppSettings["ConnectionString"];
                return _connectionString;
            }
        }

        public static void ExecuteFile(string fileName)
        {
            string sql = File.ReadAllText(fileName);
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetContentsOf(string tableName)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM {0}", tableName), cnn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cnn.Open();
                da.Fill(dataTable);
            }

            return dataTable;
        }
    }
}