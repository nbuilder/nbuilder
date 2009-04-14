using System;
using FizzWare.NBuilder.Tests.Unit.Extensibility;

namespace FizzWare.NBuilder.Tests.Unit
{
    public static class TestListBuilderExtensions
    {
        public static IOperable<T> WhereAllEven<T>(this IListBuilder<T> listBuilder)
        {
            var listBuilderImpl = listBuilder as Implementation.IListBuilderImpl<T>;

            if (listBuilderImpl == null)
                throw new ArgumentException("List builder must implement IListBuilderImpl<T>");

            return (IOperable<T>)listBuilderImpl.AddDeclaration(new EvenDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder()));
        }
    }
}