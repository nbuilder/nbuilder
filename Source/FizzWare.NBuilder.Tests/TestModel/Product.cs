using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FizzWare.NBuilder.Tests.TestModel
{
    [DebuggerDisplay("Product {Id} - {Title}")]
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int QuantityInStock { get; set; }

        public decimal PriceBeforeTax { get; set; }

        public TaxType TaxType { get; set; }

        public IList<Category> Categories { get; set; }

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
    }
}