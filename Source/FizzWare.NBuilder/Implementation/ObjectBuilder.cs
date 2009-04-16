using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public class ObjectBuilder<T> : IObjectBuilder<T>
    {
        private readonly IReflectionUtil reflectionUtil;
        private IPropertyNamer<T> propertyNamer;
        private object[] constructorArgs;

        private readonly List<Expression> functions = new List<Expression>();

        private readonly List<MultiFunction> multiFunctions = new List<MultiFunction>();

        public ObjectBuilder(IReflectionUtil reflectionUtil)
        {
            this.reflectionUtil = reflectionUtil;
        }

        public IObjectBuilder<T> WithConstructorArgs(params object[] args)
        {
            this.constructorArgs = args;
            return this;
        }

        public IObjectBuilder<T> With<TFunc>(Func<T, TFunc> func)
        {
            functions.Add(ToExpression(func));
            return this;
        }

        public IObjectBuilder<T> Do(Action<T> action)
        {
            functions.Add(ToExpression(action));
            return this;
        }

        public IObjectBuilder<T> DoMultiple<U>(Action<T, U> action, IList<U> list)
        {
            var expression = ToExpression(action);
            multiFunctions.Add(new MultiFunction(expression, list));

            return this;
        }

        static Expression<Func<V, TResult>> ToExpression<V, TResult>(Func<V, TResult> method)
        {
            return x => method(x);
        }

        static Expression<Action<V>> ToExpression<V>(Action<V> method)
        {
            return x => method(x);
        }

        static Expression<Action<V, W>> ToExpression<V, W>(Action<V, W> method)
        {
            return (x, y) => method(x, y);
        }

        public IObjectBuilder<T> WithNamingStrategy(IPropertyNamer<T> thePropertyNamer)
        {
            this.propertyNamer = thePropertyNamer;
            return this;
        }

        public T Build()
        {
            var obj = Construct();
            Name(obj);
            CallFunctions(obj);

            return obj;
        }

        public void CallFunctions(T obj)
        {
            for (int i = 0; i < functions.Count; i++)
            {
                var lambda = (LambdaExpression)functions[i];
                lambda.Compile().DynamicInvoke(obj);
            }

            for (int i = 0; i < multiFunctions.Count; i++)
            {
                multiFunctions[i].Call(obj);
            }
        }

        public T Construct()
        {
            bool requiresArgs = reflectionUtil.RequiresConstructorArgs(typeof(T));

            T obj;

            if (requiresArgs && constructorArgs != null)
            {
                obj = reflectionUtil.CreateInstanceOf<T>(constructorArgs);
            }
            else if (constructorArgs != null)
            {
                obj = reflectionUtil.CreateInstanceOf<T>(constructorArgs);
            }
            else if (!requiresArgs)
            {
                obj = reflectionUtil.CreateInstanceOf<T>();
            }
            else
            {
                throw new TypeCreationException(
                    "Type does not have a default parameterless constructor but no constructor args were specified. Use WithConstructorArgs() method to supply some the required arguments.");
            }

            return obj;
        }

        public T Name(T obj)
        {
            propertyNamer.SetValuesOf(obj);
            return obj;
        }
    }
}