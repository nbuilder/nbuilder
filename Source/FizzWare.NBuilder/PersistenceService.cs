using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FizzWare.NBuilder
{
    public class PersistenceService : IPersistenceService
    {
        public Dictionary<Type, Expression> CreateMethods { get; set; }
        public Dictionary<Type, Expression> UpdateMethods { get; set; }

        public PersistenceService()
        {
            CreateMethods = new Dictionary<Type, Expression>();
            UpdateMethods = new Dictionary<Type, Expression>();
        }

        static Expression<Action<V>> ToExpression<V>(Action<V> method)
        {
            return x => method(x);
        }

        public void Create<T>(T obj)
        {
            if (!CreateMethods.ContainsKey(typeof(T)))
                throw new PersistenceMethodNotFoundException("No persistence create method set up for " + typeof(T).Name + ". Add one using BuilderSetup.SetPersistenceCreateMethod()");

            var lambda = (LambdaExpression)CreateMethods[typeof(T)];

            lambda.Compile().DynamicInvoke(obj);
        }

        public void Create<T>(IList<T> list)
        {
            if (!CreateMethods.ContainsKey(typeof(IList<T>)))
                throw new PersistenceMethodNotFoundException("No persistence create method set up for " + typeof(IList<T>).Name + ". Add one using BuilderSetup.SetPersistenceCreateMethod()");

            var lambda = (LambdaExpression)CreateMethods[typeof(IList<T>)];

            lambda.Compile().DynamicInvoke(list);
        }

        public void Update<T>(T obj)
        {
            if (!UpdateMethods.ContainsKey(typeof(T)))
                    throw new PersistenceMethodNotFoundException("No persistence update method set up for " + typeof(T).Name + ". Add one using BuilderSetup.SetPersistenceUpdateMethod()");

            var lambda = (LambdaExpression)UpdateMethods[typeof(T)];

            lambda.Compile().DynamicInvoke(obj);
        }

        public void Update<T>(IList<T> obj)
        {
            if (!UpdateMethods.ContainsKey(typeof(IList<T>)))
                throw new PersistenceMethodNotFoundException("No persistence update method set up for " + typeof(IList<T>).Name + ". Add one using BuilderSetup.SetPersistenceUpdateMethod()");

            var lambda = (LambdaExpression)UpdateMethods[typeof(IList<T>)];

            lambda.Compile().DynamicInvoke(obj);
        }

        public void SetPersistenceCreateMethod<T>(Action<T> saveMethod)
        {
            var expr = ToExpression(saveMethod);

            if (CreateMethods.ContainsKey(typeof(T)))
                CreateMethods[typeof(T)] = expr;
            else
                CreateMethods.Add(typeof(T), expr);
        }

        public void SetPersistenceUpdateMethod<T>(Action<T> saveMethod)
        {
            var expr = ToExpression(saveMethod);

            if (UpdateMethods.ContainsKey(typeof(T)))
                UpdateMethods[typeof(T)] = expr;
            else
                UpdateMethods.Add(typeof(T), expr);
        }
    }
}