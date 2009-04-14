using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    /// <summary>
    /// This represents some kind of repository. The implementing class could update the database
    /// directly, or an ORM could be used
    /// </summary>
    public interface IRepository<T>
    {
        IList<T> GetAll();
        void Save(T objectToSave);
        void SaveAll(IEnumerable<T> rangeToSave);
        void Create(T objectToCreate);
        void CreateAll(IEnumerable<T> rangeToCreate);
        void DeleteAll();
        void Delete(T objectToDelete);
        long CountAll();
    }
}