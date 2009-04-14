using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public class CategoryRepository : BaseActiveRecordRepository<Category>, ICategoryRepository
    {
        
    }
}