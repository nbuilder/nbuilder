using System;
using System.Collections.Generic;
using FizzWare.NBuilder.PropertyValueNaming;
using System.Linq.Expressions;


namespace FizzWare.NBuilder
{
    public class Builder<T>
    {
        public static SingleObjectBuilder<T> CreateNew()
        {
            return CreateNew(new SequentialPropertyNameNamingStrategy<T>());
        }

        public static SingleObjectBuilder<T> CreateNew(IPropertyValueNamingStategy<T> strategy)
        {
            return new SingleObjectBuilder<T>(strategy);
        }

        public static SingleObjectBuilder<T> CreateNew(T prototype)
        {
            return new SingleObjectBuilder<T>(prototype);
        }

        public static ListBuilder<T> CreateListOfSize(int size)
        {
            return CreateListOfSize(size, new SequentialPropertyNameNamingStrategy<T>());
        }

        public static ListBuilder<T> CreateListOfSize(int size, IPropertyValueNamingStategy<T> namingStategy)
        {
            return new ListBuilder<T>(size, namingStategy);
        }

        public static ListBuilder<T> CreateListOfSize(int size, T basedOn)
        {
            return new ListBuilder<T>(size, basedOn);
        }

        public static ConstructorArgBuilder<T> WithConstructorArgs(params object[] args)
        {
            return new ConstructorArgBuilder<T>(args);
        }
    }

    public class ConstructorArgBuilder<T>
    {
        private readonly object[] constructorArgs;
        
        public ConstructorArgBuilder(params object[] args)
        {
            this.constructorArgs = args;
        }

        public SingleObjectBuilder<T> CreateNew()
        {
            return CreateNew(new SequentialPropertyNameNamingStrategy<T>());
        }

        public SingleObjectBuilder<T> CreateNew(IPropertyValueNamingStategy<T> strategy)
        {
            return new SingleObjectBuilder<T>(strategy);
        }

        public SingleObjectBuilder<T> CreateNew(T prototype)
        {
            return new SingleObjectBuilder<T>(prototype);
        }

        public ListBuilder<T> CreateListOfSize(int size)
        {
            return CreateListOfSize(size, new SequentialPropertyNameNamingStrategy<T>());
        }

        public ListBuilder<T> CreateListOfSize(int size, IPropertyValueNamingStategy<T> namingStategy)
        {
            return new ListBuilder<T>(size, namingStategy, constructorArgs);
        }

        public ListBuilder<T> CreateListOfSize(int size, T basedOn)
        {
            return new ListBuilder<T>(size, basedOn);
        }
    }
}