using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    public class ProductsDbContext : DbContext
    {
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<TaxType> Taxes { get; set; }  
        public virtual IDbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasMany(p => p.Categories).WithMany(c => c.Products);
        }
    }
}
