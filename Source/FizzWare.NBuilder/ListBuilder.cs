using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.PropertyValueNaming;

namespace FizzWare.NBuilder
{
    public class ListBuilder<T> 
    {
        private readonly IList<T> builtObjects = new List<T>();

        private IList<T> currentOperationSet = new List<T>();
        private readonly IList<T> operatedObjects = new List<T>();

        public ListBuilder(int size, IPropertyValueNamingStategy<T> namingStategy)
        {
            for (int i = 0; i < size; i++)
            {
                builtObjects.Add(ReflectionUtil.CreateInstanceOf<T>());
            }

            namingStategy.SetValuesOfAll(builtObjects.ToList());
        }

        public ListBuilder(int size, IPropertyValueNamingStategy<T> namingStategy, params object[] args)
        {
            for (int i = 0; i < size; i++)
            {
                builtObjects.Add(ReflectionUtil.CreateInstanceOf<T>(args));
            }

            namingStategy.SetValuesOfAll(builtObjects.ToList());
        }

        public ListBuilder(int size, T basedOn)
        {
            for (int i = 0; i < size; i++)
            {
                builtObjects.Add(basedOn);
            }
        }

        public ListBuilder<T> WhereRandom(int count)
        {
            IList<T> allowedItems = builtObjects.Except(operatedObjects).ToList();

            List<T> values = new List<T>();

            var generator = new UniqueRandomGenerator<int>(0, allowedItems.Count);
            
            for (int i = 0; i < count; i++)
            {
                values.Add(allowedItems[generator.Generate()]);
            }

            currentOperationSet = values;

            return this;
        }

        public ListBuilder<T> WhereTheFirst(int count)
        {
            currentOperationSet = builtObjects.Take(count).ToList();

            foreach (var t in currentOperationSet)
                operatedObjects.Add(t);
            
            return this;
        }

        public ListBuilder<T> WhereTheLast(int count)
        {
            currentOperationSet = builtObjects.Skip(builtObjects.Count - count).ToList();

            foreach (var t in currentOperationSet)
                operatedObjects.Add(t);

            return this;
        }

        public ListBuilder<T> WhereAll()
        {
            currentOperationSet = builtObjects;

            return this;
        }

        public ListBuilder<T> Have<TFunc>(Func<T, TFunc> func)
        {
            //if (currentOperationSet == null)
            //    throw new Exception("Have used before WhereTheFirst(), WhereAll()");

            foreach (var t in currentOperationSet)
                func(t);

            return this;
        }

        public ListBuilder<T> Has<TFunc>(Func<T, TFunc> func)
        {
            return Have(func);
        }

        public ListBuilder<T> And<TFunc>(Func<T, TFunc> func)
        {
            foreach (var t in currentOperationSet)
                func(t);

            return this;
        }

        public ListBuilder<T> And(Action<T> action)
        {
            foreach (var t in currentOperationSet)
                action(t);

            return this;
        }

        public ListBuilder<T> AndTheNext(int count)
        {
            var allowedItems = builtObjects.Except(operatedObjects);

            currentOperationSet = allowedItems.Take(count).ToList();

            foreach (var t in currentOperationSet)
                operatedObjects.Add(t);

            return this;
        }

        public ListBuilder<T> BasedOn(T basedOnObj)
        {
            var type = typeof (T);

            foreach (var obj in builtObjects)
            {
                foreach (var propertyInfo in type.GetProperties())
                {
                    var value = propertyInfo.GetValue(basedOnObj, null);

                    if (propertyInfo.CanWrite)
                        propertyInfo.SetValue(obj, value , null);
                }
            }

            return this;
        }

        public ListBuilder<T> HaveDoneToThem(Action<T> action)
        {
            foreach (var t in currentOperationSet)
                action(t);

            return this;
        }

        public ListBuilder<T> HasDoneToIt(Action<T> action)
        {
            return HaveDoneToThem(action);
        }
    
        public IList<T> Build()
        {
            return builtObjects;
        }
    }
}