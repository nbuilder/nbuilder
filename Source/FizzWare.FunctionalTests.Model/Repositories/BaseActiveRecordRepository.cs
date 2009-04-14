using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public abstract class BaseActiveRecordRepository<T> : IRepository<T> where T : class
    {
        public IList<T> GetAll()
        {
            return ActiveRecordMediator<T>.FindAll();
        }

        public void Save(T objectToSave)
        {
            ActiveRecordMediator<T>.Save(objectToSave);
        }

        public void SaveAll(IEnumerable<T> rangeToSave)
        {
            foreach (var obj in rangeToSave)
                ActiveRecordMediator<T>.Save(obj);
        }

        public void Create(T objectToCreate)
        {
            ActiveRecordMediator<T>.Create(objectToCreate);
        }

        public void CreateAll(IEnumerable<T> rangeToCreate)
        {
            foreach (var obj in rangeToCreate)
                ActiveRecordMediator<T>.Create(obj);
        }
        
        public void DeleteAll()
        {
            ActiveRecordMediator<T>.DeleteAll();
        }

        public void Delete(T objectToDelete)
        {
            ActiveRecordMediator<T>.Delete(objectToDelete);
        }

        public long CountAll()
        {
            return ActiveRecordMediator<T>.Count();
        }
    }
}