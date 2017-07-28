using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void DeleteAllWithTaxType(TaxType taxType);

        long CountAllInCategory(int categoryId);
        IList<Product> GetRangeInCategory(int categoryId, int start, int end);
    }
}