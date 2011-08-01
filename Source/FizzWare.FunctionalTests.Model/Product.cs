using System;
using Castle.ActiveRecord;
using System.Collections;
using System.Collections.Generic;
using FizzWare.NBuilder.FunctionalTests.Model;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    [ActiveRecord]
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

        [PrimaryKey(Generator = PrimaryKeyType.Native)]
        public int Id { get; set; }

        [Property]
        public string Title { get; set; }

        [Property]
        public string Description { get; set; }

        [Property]
        public int QuantityInStock { get; set; }
        
        [Property]
        public decimal PriceBeforeTax { get; set; }

		[Property]
		public double? Weight { get; set; }

        [BelongsTo(Column = "TaxTypeId", Type = typeof(TaxType), Cascade = CascadeEnum.All)]
        public TaxType TaxType { get; set; }

        [HasAndBelongsToMany(typeof(Category),
            Table = "ProductCategory", 
            ColumnKey = "ProductId", 
            ColumnRef = "CategoryId", 
            Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<Category> Categories { get; set; }

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