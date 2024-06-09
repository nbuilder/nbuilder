using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.Extensions;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public class ObjectBuilder<T> : IObjectBuilder<T>
    {
        private readonly IReflectionUtil reflectionUtil;
        private IPropertyNamer propertyNamer;

        private readonly List<MulticastDelegate> functions = new List<MulticastDelegate>();

        private readonly List<MultiFunction> multiFunctions = new List<MultiFunction>();
        private Func<T> _factoryFunction;
        private Func<int, T> _indexedFactoryFunction;
        public BuilderSettings BuilderSettings { get; set; }

        public ObjectBuilder()
        {
            BuilderSettings = new BuilderSettings
            {
                AutoNameProperties = true,
            };
            this.reflectionUtil = new ReflectionUtil();
            propertyNamer = BuilderSettings.GetPropertyNamerFor<T>();
        }
        public ObjectBuilder(IReflectionUtil reflectionUtil, BuilderSettings builderSettings)
        {
            BuilderSettings = builderSettings;
            this.reflectionUtil = reflectionUtil;
        }

        public IObjectBuilder<T> WithFactory(Func<T> factory)
        {
            this._factoryFunction = factory;
            return this;
        }

        [Obsolete("Use WithFactory instead.")]
        public IObjectBuilder<T> WithConstructor(Func<T> constructor)
        {
            return this.WithFactory(constructor);
        }

        public IObjectBuilder<T> WithFactory(Func<int, T> constructor)
        {
            this._indexedFactoryFunction = constructor;
            return this;
        }
    
        [Obsolete("Use WithFactory instead.")]
        public IObjectBuilder<T> WithConstructor(Func<int, T> constructor)
        {
            return this.WithFactory(constructor);
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

        public T BuildRecursive()
        {
            var obj = Build();
            
            var t = typeof(T);

            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.GetValue(obj,null) != null)
                 continue;

                var propType = propertyInfo.PropertyType;

                // Create an instance of the generic class with the specified type
                Type genericClassType = typeof(ObjectBuilder<>);
                Type constructedClassType = genericClassType.MakeGenericType(propType);

                // Create an instance of the constructed type
                object instance = Activator.CreateInstance(constructedClassType);
                MethodInfo methodInfo = constructedClassType.GetTypeInfo().GetMethods().FirstOrDefault(x => x.Name == "BuildRecursive");
                if (methodInfo == null)
                {
                    Console.WriteLine("Method not found.");
                    return default(T);
                }

                // Invoke the BuildRecursive method on the instance
                var result = methodInfo.Invoke(instance, null);
              
                propertyInfo.SetValue(obj, result, null);
            }

            return obj;
        }
        

        public void CallFunctions(T obj)
        {
            CallFunctions(obj, 0);
        }

        public void CallFunctions(T obj, int objIndex)
        {
            foreach (var del in functions)
            {
                var parameterCount = del.GetMethodInfo().GetParameters().Count();
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


            foreach (var t in multiFunctions)
            {
                t.Call(obj);
            }
        }

        public T Construct(int index)
        {
            var obj = Construct(index,typeof(T));
            return (T) obj;
        }
        public object Construct(int index, Type objType)
        {
            var requiresArgs = reflectionUtil.RequiresConstructorArgs(objType);

            if (objType.IsInterface())
                throw new TypeCreationException("Cannot build an interface");

            if (objType.IsAbstract())
                throw new TypeCreationException("Cannot build an abstract class");

            if (_factoryFunction != null)
            {
                return _factoryFunction.Invoke();
            }

            if (_indexedFactoryFunction != null)
            {
                return _indexedFactoryFunction.Invoke(index);
            }

            
            return  reflectionUtil.CreateInstanceOf(objType);
        }
        public T Name(T obj)
        {
 
            if (!BuilderSettings.AutoNameProperties)
                return obj;

            propertyNamer.SetValuesOf(obj);
            return obj;
        }
    }
}
