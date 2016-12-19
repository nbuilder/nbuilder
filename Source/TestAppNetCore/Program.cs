using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;

namespace TestAppNetCore
{

    class Product
    {
        public int i { get; set; }
        public String s { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builderSetup = new BuilderSettings();
            var products = new Builder(builderSetup).CreateListOfSize<Product>(10).Build();

            foreach (var product in products)
            {
                Console.WriteLine(product.i + " - " + product.s);
            }
        }

    }
}
