using System;
using FizzWare.NBuilder.PropertyValueNaming;
using System.Reflection;
using System.Collections;

namespace FizzWare.NBuilder
{
    public class SingleObjectBuilder<T>
    {
        private readonly ObjectBuilder<T> objectBuilder;

        public SingleObjectBuilder(INamingStrategy<T> strategy, IReflectionUtil reflectionUtil)
        {
            objectBuilder = new ObjectBuilder<T>(reflectionUtil);
            objectBuilder.WithNamingStrategy(strategy);
        }

        public SingleObjectBuilder(T basedOn)
        {
            
        }

        public SingleObjectBuilder<T> WithConstructorArgs(params object[] args)
        {
            objectBuilder.WithConstructorArgs(args);
            return this;
        }

        //public SingleObjectBuilder<T> With<TFunc>(Func<T, TFunc> func)
        //{
        //    func(builtObj);
        //    return this;
        //}

        //public SingleObjectBuilder<T> And<TFunc>(Func<T, TFunc> func)
        //{
        //    func(builtObj);
        //    return this;
        //}

        //public SingleObjectBuilder<T> And(Action<T> action)
        //{
        //    action(builtObj);
        //    return this;
        //}

        //public SingleObjectBuilder<T> Do(Action<T> action)
        //{
        //    action(builtObj);
        //    return this;
        //}

        public T Build()
        {
            return objectBuilder.Build();
        }
    }
}