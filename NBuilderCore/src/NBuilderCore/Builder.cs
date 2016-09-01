using NBuilderCore.Implementation;
using NBuilderCore.PropertyNaming;

namespace NBuilderCore
{
    public class Builder<T>
    {
        private readonly BuilderSetup _builderSetup;

        public Builder(BuilderSetup builderSetup)
        {
            _builderSetup = builderSetup;
        }

        public ISingleObjectBuilder<T> CreateNew()
        {
          
            var reflectionUtil = new ReflectionUtil();
            var propertyNamer = _builderSetup.GetPropertyNamerFor<T>();
            return new ObjectBuilder<T>(reflectionUtil, _builderSetup).WithPropertyNamer(propertyNamer);
        }

        public IListBuilder<T> CreateListOfSize(int size)
        {
            Guard.Against(size < 1, "Size of list must be 1 or greater");
            var propertyNamer = _builderSetup.GetPropertyNamerFor<T>();
            return CreateListOfSize(size, propertyNamer);
        }

        public IListBuilder<T> CreateListOfSize(int size, IPropertyNamer propertyNamer)
        {
            return new ListBuilder<T>(size, propertyNamer, new ReflectionUtil(),_builderSetup);
        }
    }
}