using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FizzWare.NBuilder
{
    class OperationSet<T>
    {
        private readonly int number;
        private readonly SetType type;
        private object[] constructorArgs;
        private readonly List<LambdaExpression> functions = new List<LambdaExpression>();

        private List<T> list;

        public OperationSet(int number, SetType type)
        {
            this.number = number;
            this.type = type;
        }

        public SetType Type
        {
            get { return type; }
        }

        public void HaveConstructorArgs(params object[] args)
        {
            constructorArgs = args;
        }

        public void Have<TFunc>(Func<T, TFunc> func)
        {
            EnsureList();
            list.ForEach(x => func(x));
        }

        public void Has<TFunc>(Func<T, TFunc> func)
        {
            Have(func);
        }

        private void EnsureList()
        {
            if (list == null)
            {
                list = new List<T>(number);

                if (constructorArgs != null)
                {
                    var item = CreateNew(constructorArgs);
                    list.Add(item);
                }
                else
                {
                    list.Add(CreateNew());
                }
            }
        }

        public IList<T> Build()
        {

            return list;
        }

        private static T CreateNew(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        private static T CreateNew()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}