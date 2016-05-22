using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FizzWare.NBuilder.FunctionalTests.Model;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    
    public class Product : IProduct
    {
        public Product()
        {
            Categories = new List<Category>();
        }

        public Product(string title)
            : this()
        {
            Title = title;
        }

        [Key]
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }

        
        public int QuantityInStock { get; set; }
        
        
        public decimal PriceBeforeTax { get; set; }

        
        public double? Weight { get; set; }

        public virtual TaxType TaxType { get; set; }

        public virtual List<Category> Categories { get; set; }

        public WarehouseLocation Location { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastEdited { get; set; }

        public decimal Tax
        { 
            get
            {
                if (TaxType != null)
                    return PriceBeforeTax * TaxType.Percentage;

                return 0m;
            }
        }

        public decimal PriceAfterTax
        {
            get
            {
                if (TaxType != null)
                    return PriceBeforeTax + Tax;

                return PriceBeforeTax;
 
            }
        }

        public void AddToCategory(Category category)
        {
            this.Categories.Add(category);
        }
    }
}