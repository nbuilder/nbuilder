using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    public class Builder<T>
    {
        public static ISingleObjectBuilder<T> CreateNew()
        {
            var reflectionUtil = new ReflectionUtil();
            var propertyNamer = BuilderSetup.GetPropertyNamerFor<T>();
            return new ObjectBuilder<T>(reflectionUtil).WithPropertyNamer(propertyNamer);
        }

        public static IListBuilder<T> CreateListOfSize(int size)
        {
            Guard.Against(size < 1, "Size of list must be 1 or greater");
            var propertyNamer = BuilderSetup.GetPropertyNamerFor<T>();
            return CreateListOfSize(size, propertyNamer);
        }

        public static IListBuilder<T> CreateListOfSize(int size, IPropertyNamer propertyNamer)
        {
            return new ListBuilder<T>(size, propertyNamer, new ReflectionUtil());
        }
    }
}