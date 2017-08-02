using System.Collections.Generic;

namespace FizzWare.NBuilder.Tests.Integration.Models.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void DeleteAllWithTaxType(TaxType taxType);

        long CountAllInCategory(int categoryId);
        IList<Product> GetRangeInCategory(int categoryId, int start, int end);
    }
}