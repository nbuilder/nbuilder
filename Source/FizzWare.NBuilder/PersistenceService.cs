using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FizzWare.NBuilder
{
    public class PersistenceService : IPersistenceService
    {
        public Dictionary<Type, Expression> Persisters { get; set; }

        public PersistenceService()
        {
            Persisters = new Dictionary<Type, Expression>();
        }

        public void SetPersistenceMethod<T>(Action<IList<T>> saveMethod)
        {
            var expr = ToExpression(saveMethod);

            if (Persisters.ContainsKey(typeof(IList<T>)))
                Persisters[typeof (IList<T>)] = expr;
            else
                Persisters.Add(typeof(IList<T>), expr);
        }

        public void SetPersistenceMethod<T>(Action<T> saveMethod)
        {
            var expr = ToExpression(saveMethod);

            if (Persisters.ContainsKey(typeof(T)))
                Persisters[typeof(T)] = expr;
            else
                Persisters.Add(typeof(T), expr);
        }

        static Expression<Action<V>> ToExpression<V>(Action<V> method)
        {
            return x => method(x);
        }

        public void Persist<T>(T obj)
        {
            var lambda = (LambdaExpression) Persisters[typeof (T)];
            lambda.Compile().DynamicInvoke(obj);
        }

        public void Persist<T>(IList<T> obj)
        {
            var lambda = (LambdaExpression)Persisters[typeof(IList<T>)];
            lambda.Compile().DynamicInvoke(obj);
        }
    }
}