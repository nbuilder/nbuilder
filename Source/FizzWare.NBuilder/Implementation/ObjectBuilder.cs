using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public class ObjectBuilder<T> : IObjectBuilder<T>
    {
        private readonly IReflectionUtil reflectionUtil;
        private IPropertyNamer propertyNamer;
        private object[] constructorArgs;

        private readonly List<MulticastDelegate> functions = new List<MulticastDelegate>();

        private readonly List<MultiFunction> multiFunctions = new List<MultiFunction>();
        private Expression<Func<int, T>> _constructorExpression = null;
        public BuilderSetup BuilderSetup { get; set; }
        public ObjectBuilder(IReflectionUtil reflectionUtil, BuilderSetup builderSetup)
        {
            BuilderSetup = builderSetup;
            this.reflectionUtil = reflectionUtil;
        }

        public IObjectBuilder<T> WithConstructor(Expression<Func<T>> constructor)
        {
            if (constructor.Body.NodeType != ExpressionType.New)
            {
                throw new ArgumentException("WithConstructor expects a constructor expression");
            }

            var constructorArguments =
                (from argument in ((NewExpression)constructor.Body).Arguments
                select Expression.Lambda(argument).Compile().DynamicInvoke()).ToArray();

            constructorArgs = constructorArguments;
            return this;
        }

        public IObjectBuilder<T> WithConstructor(Expression<Func<int, T>> constructor)
        {
            if (constructor.Body.NodeType != ExpressionType.New)
            {
                throw new ArgumentException("WithConstructor expects a constructor expression");
            }

            this._constructorExpression = constructor;

            //var arguments = ((NewExpression) constructor.Body).Arguments;
            //var indexArgument = arguments[0];
            //var index = Expression.Lambda(indexArgument).Compile().DynamicInvoke();

            //var theOtherArguments = arguments.Skip(1).ToArray();

            //var constructorArguments =
            //    (from argument in theOtherArguments
            //     select Expression.Lambda(argument).Compile().DynamicInvoke()).ToArray();

            //constructorArgs = constructorArguments;
            return this;
        }

        // This has been obsolete for a while, so don't allow this one to be hidden
        [Obsolete]
        public IObjectBuilder<T> WithConstructorArgs(params object[] args)
        {
            this.constructorArgs = args;
            return this;
        }

        public IObjectBuilder<T> With<TFunc>(Func<T, TFunc> func)
        {
            functions.Add(func);
            return this;
        }

        public IObjectBuilder<T> With(Action<T, int> func)
        {
            functions.Add(func);
            return this;
        }

        public IObjectBuilder<T> Do(Action<T> action)
        {
            functions.Add(action);
            return this;
        }

        public IObjectBuilder<T> Do(Action<T, int> action)
        {
            functions.Add(action);
            return this;
        }

        public IObjectBuilder<T> DoMultiple<U>(Action<T, U> action, IList<U> list)
        {
            multiFunctions.Add(new MultiFunction(action, list));
            return this;
        }

        public IObjectBuilder<T> WithPropertyNamer(IPropertyNamer thePropertyNamer)
        {
            this.propertyNamer = thePropertyNamer;
            return this;
        }

        public T Build()
        {
            var obj = Construct(1);
            Name(obj);
            CallFunctions(obj);

            return obj;
        }

        public void CallFunctions(T obj)
        {
            CallFunctions(obj, 0);
        }

        public void CallFunctions(T obj, int objIndex)
        {
            for (int i = 0; i < functions.Count; i++)
            {
                var del = functions[i];
                int parameterCount = del.Method.GetParameters().Count();
                switch (parameterCount)
                {
                    case 1:
                        del.DynamicInvoke(obj);
                        break;
                    case 2:
                        del.DynamicInvoke(new object[] { obj, objIndex });
                        break;
                }
            }

            for (int i = 0; i < multiFunctions.Count; i++)
            {
                multiFunctions[i].Call(obj);
            }
        }

        public T Construct(int index)
        {
            bool requiresArgs = reflectionUtil.RequiresConstructorArgs(typeof(T));

            if (typeof(T).IsInterface)
                throw new TypeCreationException("Cannot build an interface");

            if (typeof(T).IsAbstract)
                throw new TypeCreationException("Cannot build an abstract class");

            T obj;

            if (_constructorExpression != null)
            {
                obj = _constructorExpression.Compile().Invoke(index);
            }
            else if (requiresArgs && constructorArgs != null)
            {
                obj = reflectionUtil.CreateInstanceOf<T>(constructorArgs);
            }
            else if (constructorArgs != null)
            {
                obj = reflectionUtil.CreateInstanceOf<T>(constructorArgs);
            }
            else
            {
                obj = reflectionUtil.CreateInstanceOf<T>();
            }

            return obj;
        }

        public T Name(T obj)
        {
            if (!BuilderSetup.AutoNameProperties)
                return obj;

            propertyNamer.SetValuesOf(obj);
            return obj;
        }
    }
}