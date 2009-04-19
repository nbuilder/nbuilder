using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FizzWare.NBuilder
{
    public delegate void PersistenceMethod<T>(T obj);

    public class PersistenceService : IPersistenceService
    {
        public Dictionary<Type, MulticastDelegate> CreateMethods { get; set; }
        public Dictionary<Type, MulticastDelegate> UpdateMethods { get; set; }

        public PersistenceService()
        {
            CreateMethods = new Dictionary<Type, MulticastDelegate>();
            UpdateMethods = new Dictionary<Type, MulticastDelegate>();
        }

        public void Create<T>(T obj)
        {
            if (!CreateMethods.ContainsKey(typeof(T)))
                throw new PersistenceMethodNotFoundException("No persistence create method set up for " + typeof(T).Name + ". Add one using BuilderSetup.SetPersistenceCreateMethod()");

            var creator = CreateMethods[typeof(T)];

            creator.DynamicInvoke(obj);
        }

        public void Create<T>(IList<T> list)
        {
            if (!CreateMethods.ContainsKey(typeof(IList<T>)))
                throw new PersistenceMethodNotFoundException("No persistence create method set up for " + typeof(IList<T>).Name + ". Add one using BuilderSetup.SetPersistenceCreateMethod()");

            var creator = CreateMethods[typeof(IList<T>)];

            creator.DynamicInvoke(list);
        }

        public void Update<T>(T obj)
        {
            if (!UpdateMethods.ContainsKey(typeof(T)))
                    throw new PersistenceMethodNotFoundException("No persistence update method set up for " + typeof(T).Name + ". Add one using BuilderSetup.SetPersistenceUpdateMethod()");

            var updater = UpdateMethods[typeof(T)];

            updater.DynamicInvoke(obj);
        }

        public void Update<T>(IList<T> obj)
        {
            if (!UpdateMethods.ContainsKey(typeof(IList<T>)))
                throw new PersistenceMethodNotFoundException("No persistence update method set up for " + typeof(IList<T>).Name + ". Add one using BuilderSetup.SetPersistenceUpdateMethod()");

            var updater = UpdateMethods[typeof(IList<T>)];

            updater.DynamicInvoke(obj);
        }

        public void SetPersistenceCreateMethod<T>(Action<T> saveMethod)
        {
            if (CreateMethods.ContainsKey(typeof(T)))
                CreateMethods[typeof(T)] = saveMethod;
            else
                CreateMethods.Add(typeof(T), saveMethod);
        }

        public void SetPersistenceUpdateMethod<T>(Action<T> saveMethod)
        {
            if (UpdateMethods.ContainsKey(typeof(T)))
                UpdateMethods[typeof(T)] = saveMethod;
            else
                UpdateMethods.Add(typeof(T), saveMethod);
        }
    }
}