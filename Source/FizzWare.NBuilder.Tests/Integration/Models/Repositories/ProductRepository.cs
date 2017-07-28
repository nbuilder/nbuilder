using System.Collections.Generic;
using System.Linq;

namespace FizzWare.NBuilder.Tests.Integration.Models.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public void DeleteAllWithTaxType(TaxType taxType)
        {
            var query = Data.Where(row => row.TaxType == taxType).ToList();
            query.ForEach(Delete);
        }

        public long CountAllInCategory(int categoryId)
        {
            var query = from p in Data
                from pc in p.Categories
                where pc.Id == categoryId
                select p;

            return query.Count();
        }

        public IList<Product> GetRangeInCategory(int categoryId, int start, int end)
        {
            var query = from p in Data
                        from pc in p.Categories
                        where pc.Id == categoryId
                        select p;

            return query.Skip(start).Take(end).ToList();


        }
    }
}