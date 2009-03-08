using System;
using FizzWare.NBuilder.PropertyValueNaming;
using System.Linq.Expressions;
using System.Reflection;

namespace FizzWare.NBuilder
{
    public class SingleObjectBuilder<T>
    {
        private readonly T builtObj;

        public SingleObjectBuilder(IPropertyValueNamingStategy<T> stategy)
        {
            builtObj = ReflectionUtil.CreateInstanceOf<T>();
            stategy.SetValuesOf(builtObj);
        }

        public SingleObjectBuilder(IPropertyValueNamingStategy<T> strategy, params object[] args)
        {
            builtObj = ReflectionUtil.CreateInstanceOf<T>(args);
            strategy.SetValuesOf(builtObj);
        }

        public SingleObjectBuilder(T basedOn)
        {
            builtObj = basedOn;
        }

        public SingleObjectBuilder<T> With<TFunc>(Func<T, TFunc> func)
        {
            func(builtObj);
            return this;
        }

        public SingleObjectBuilder<T> And<TFunc>(Func<T, TFunc> func)
        {
            func(builtObj);
            return this;
        }

        public SingleObjectBuilder<T> And(Action<T> action)
        {
            action(builtObj);
            return this;
        }

        public SingleObjectBuilder<T> Do(Action<T> action)
        {
            action(builtObj);
            return this;
        }

        public T Build()
        {
            return builtObj;
        }
    }
}