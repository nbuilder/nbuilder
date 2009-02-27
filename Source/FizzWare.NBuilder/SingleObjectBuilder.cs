using System;
using FizzWare.NBuilder.PropertyValueNaming;

namespace FizzWare.NBuilder
{
    public class SingleObjectBuilder<T> where T : new()
    {
        private readonly T builtObj;

        public SingleObjectBuilder(IPropertyValueNamingStategy<T> stategy)
        {
            builtObj = new T();
            stategy.SetValue(builtObj);
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

        public T Value
        {
            get
            {
                return builtObj;
            }
        }
    }
}