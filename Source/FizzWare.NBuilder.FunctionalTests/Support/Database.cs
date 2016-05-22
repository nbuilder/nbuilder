using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using FizzWare.NBuilder.FunctionalTests.Model;

namespace FizzWare.NBuilder.FunctionalTests.Support
{
    public class Database
    {
        public static class Tables
        {
            public static string Product = "Products";
            public static string Category = "Categories";
            public static string TaxType = "TaxType";
            public static string ShoppingBasket = "ShoppingBasket";
            public static string BasketItem = "BasketItem";
            public static string ProductCategory = "ProductCategories";
        }

        public static DataTable GetContentsOf(string tableName)
        {

            using (var dbContext = new ProductsDbContext())
            using (var connection = new SqlCeConnection(dbContext.Database.Connection.ConnectionString))
            using (var command = connection.CreateCommand())
            {

                var sql = "SELECT* FROM " + tableName;
                command.CommandText = sql;

                var adapter = new SqlCeDataAdapter(command);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                return dataSet.Tables[0];
            }

            //var connection = new SqlConnection(ConnectionString);

            //using (connection)
            //{
            //    connection.Open();
            //    var command = new SqlCommand("SELECT * FROM " + tableName, connection);

            //    var adapter = new SqlDataAdapter(command);
            //    var dataSet = new DataSet();
            //    adapter.Fill(dataSet);

            //    return dataSet.Tables[0];
            //}
        }
    }
}