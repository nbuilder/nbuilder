using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;

namespace FizzWare.NBuilder.FunctionalTests.Model.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public BaseRepository()
        {
            this.DbContext = new ProductsDbContext();
        }

        public ProductsDbContext DbContext { get; set; }

        public IList<T> GetAll()
        {
            return DbContext.Set<T>().ToList();
        }

        public void Save(T objectToSave)
        {
            this.DbContext.Set<T>().Attach(objectToSave);
            var result = this.DbContext.SaveChanges();
            if (result == 0)
            {
                throw new InvalidOperationException("Object not saved.");
            }
        }

        public void SaveAll(IEnumerable<T> rangeToSave)
        {
            var dbSet = this.DbContext.Set<T>();
            rangeToSave.ToList().ForEach(item =>
            {
                dbSet.Attach(item);
                var entry = this.DbContext.Entry(item);
                if (entry.State == EntityState.Unchanged)
                {
                    entry.State = EntityState.Modified;
                }

            });
            var result = this.DbContext.SaveChanges();
            if (result == 0)
            {
                throw new InvalidOperationException("Object not saved.");
            }
        }

        public void Create(T objectToCreate)
        {
            this.DbContext.Set<T>().Add(objectToCreate);
            var result = this.DbContext.SaveChanges();
            if (result == 0)
            {
                throw new InvalidOperationException("Object not saved.");
            }
            this.DbContext.SaveChanges();
        }

        public void CreateAll(IEnumerable<T> rangeToCreate)
        {
            rangeToCreate.ToList().ForEach(item =>
            {
                this.DbContext.Set<T>().Attach(item);
                this.DbContext.Set<T>().Add(item);
            });
            var result = this.DbContext.SaveChanges();
            if (result == 0)
            {
                throw new InvalidOperationException("Object not saved.");
            }
        }

        public void DeleteAll()
        {
            var query = this.DbContext.Set<T>().ToList();
            foreach (var item in query)
            {
                this.DbContext.Set<T>().Remove(item);
            }
            this.DbContext.SaveChanges();
        }

        public void Delete(T objectToDelete)
        {
            this.DbContext.Set<T>().Remove(objectToDelete);
            this.DbContext.SaveChanges();
        }

        public long CountAll()
        {
            return this.DbContext.Set<T>().Count();
        }
    }
}