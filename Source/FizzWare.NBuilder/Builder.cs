using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using System.Linq.Expressions;


namespace FizzWare.NBuilder
{
    public class Builder<T>
    {
        public static ISingleObjectBuilder<T> CreateNew()
        {
            var reflectionUtil = new ReflectionUtil();

            return new ObjectBuilder<T>(reflectionUtil).WithNamingStrategy(new SequentialPropertyNamer<T>(reflectionUtil));
        }

        public static IListBuilder<T> CreateListOfSize(int size)
        {
            Guard.Against(size < 1, "Size of list must be 1 or greater");

            return CreateListOfSize(size, new SequentialPropertyNamer<T>(new ReflectionUtil()));
        }

        public static IListBuilder<T> CreateListOfSize(int size, IPropertyNamer<T> propertyNamer)
        {
            return new ListBuilder<T>(size, propertyNamer, new ReflectionUtil());
        }
    }
}