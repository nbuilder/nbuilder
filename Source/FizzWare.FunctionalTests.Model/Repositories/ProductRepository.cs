using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public void DeleteAllWithTaxType(TaxType taxType)
        {

            var query = this.DbContext.Set<Product>().Where(row => row.TaxType == taxType).ToList();
            foreach (var item in query)
            {
                this.DbContext.Set<Product>().Remove(item);
            }
            this.DbContext.SaveChanges();
        }

        public long CountAllInCategory(int categoryId)
        {
            var query = from p in this.DbContext.Set<Product>()
                from pc in p.Categories
                where pc.Id == categoryId
                select p;

            return query.Count();
        }

        public IList<Product> GetRangeInCategory(int categoryId, int start, int end)
        {
            var query = from p in this.DbContext.Set<Product>()
                        from pc in p.Categories
                        where pc.Id == categoryId
                        select p;

            return query.Skip(start).Take(end).ToList();


            //var criteria = DetachedCriteria.For<Product>()
            //    .CreateCriteria("Categories")
            //    .Add(Expression.Eq("Id", categoryId));            
            
            //return (IList<Product>)ActiveRecordMediator.SlicedFindAll(typeof (Product), start, end, criteria);
        }
    }
}