using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FizzWare.NBuilder.FunctionalTests.Support
{
    public class Database
    {
        public static class Tables
        {
            public static string Product = "Product";
            public static string Category = "Category";
            public static string TaxType = "TaxType";
            public static string ShoppingBasket = "ShoppingBasket";
            public static string BasketItem = "BasketItem";
            public static string ProductCategory = "ProductCategory";
        }

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public static void Clear()
        {
            DeleteFrom(Tables.ProductCategory);
            DeleteFrom(Tables.Product);
            DeleteFrom(Tables.Category);
            DeleteFrom(Tables.TaxType);
            DeleteFrom(Tables.ShoppingBasket);
            DeleteFrom(Tables.BasketItem);
        }

        private static void Truncate(string tableName)
        {
            ExecuteNonQuery("TRUNCATE TABLE [{0}]", tableName);
        }

        private static void ExecuteNonQuery(string sql, params object[] parameters)
        {
            ExecuteNonQuery(string.Format(sql, parameters));
        }

        private static void ExecuteNonQuery(string sql)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        private static void DeleteFrom(string tableName)
        {
            ExecuteNonQuery("DELETE FROM [{0}]", tableName);
        }

        public static DataTable GetContentsOf(string tableName)
        {
            var connection = new SqlConnection(ConnectionString);

            using (connection)
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM " + tableName, connection);

                var adapter = new SqlDataAdapter(command);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                return dataSet.Tables[0];
            }
        }
    }
}