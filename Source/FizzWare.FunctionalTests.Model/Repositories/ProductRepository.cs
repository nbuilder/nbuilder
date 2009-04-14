using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using NHibernate.Expression;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public class ProductRepository : BaseActiveRecordRepository<Product>, IProductRepository
    {
        public void DeleteAllWithTaxType(TaxType taxType)
        {
            ActiveRecordMediator<Product>.DeleteAll(string.Format("taxTypeId = {0}", taxType.Id));
        }

        public long CountAllInCategory(int categoryId)
        {
            var criteria = DetachedCriteria.For<Product>()
                .CreateCriteria("Categories")
                .Add(Expression.Eq("Id", categoryId));

            return ActiveRecordMediator.Count(typeof (Product), criteria);
        }

        public IList<Product> GetRangeInCategory(int categoryId, int start, int end)
        {
            var criteria = DetachedCriteria.For<Product>()
                .CreateCriteria("Categories")
                .Add(Expression.Eq("Id", categoryId));            
            
            return (IList<Product>)ActiveRecordMediator.SlicedFindAll(typeof (Product), start, end, criteria);
        }
    }
}