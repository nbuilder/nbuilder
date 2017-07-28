using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected static readonly List<T> Data = new List<T>();

        public IList<T> GetAll()
        {
            return Data.AsReadOnly();
        }

        public void Save(T objectToSave)
        {
            Data.Add(objectToSave);
        }

        public void SaveAll(IEnumerable<T> rangeToSave)
        {
            rangeToSave.ToList().ForEach(Save);
        }

        public void Create(T objectToCreate)
        {
            Data.Add(objectToCreate);
        }

        public void CreateAll(IEnumerable<T> rangeToCreate)
        {
            rangeToCreate.ToList().ForEach(Create);
        }

        public void DeleteAll()
        {
            Data.Clear();
        }

        public void Delete(T objectToDelete)
        {
            Data.Remove(objectToDelete);
        }

        public long CountAll()
        {
            return Data.Count;
        }
    }
}