using System;
using System.Collections.Generic;
using FizzWare.NBuilder.PropertyValueNaming;
using System.Linq.Expressions;


namespace FizzWare.NBuilder
{
    public class Builder<T> where T : new()
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
    }
}