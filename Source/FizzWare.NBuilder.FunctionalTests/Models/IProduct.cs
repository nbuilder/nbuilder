using System.Collections.Generic;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    public interface IProduct
    {
        int Id { get; set; }

        string Title { get; set; }

        string Description { get; set; }

        int QuantityInStock { get; set; }

        decimal PriceBeforeTax { get; set; }

        TaxType TaxType { get; set; }

        List<Category> Categories { get; set; }

        decimal Tax { get; }
        decimal PriceAfterTax { get; }
        void AddToCategory(Category category);
    }
}