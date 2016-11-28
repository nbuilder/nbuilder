using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{

    public static class Builder<T>
    {

        public static ISingleObjectBuilder<T> CreateNew()
        {

            var reflectionUtil = new ReflectionUtil();
            var propertyNamer = BuilderSetup.GetPropertyNamerFor<T>();
            return new ObjectBuilder<T>(reflectionUtil, BuilderSetup.Instance).WithPropertyNamer(propertyNamer);
        }

        public static IListBuilder<T> CreateListOfSize(int size)
        {
            Guard.Against(size < 1, "Size of list must be 1 or greater");
            var propertyNamer = BuilderSetup.GetPropertyNamerFor<T>();
            return CreateListOfSize(size, propertyNamer);
        }

        public static IListBuilder<T> CreateListOfSize(int size, IPropertyNamer propertyNamer)
        {
            return new ListBuilder<T>(size, propertyNamer, new ReflectionUtil(), BuilderSetup.Instance);
        }
    }



    public class Builder
    {
        private readonly BuilderSettings BuilderSettings;

        public Builder() : this(new BuilderSettings())
        {
            
        } 

        public Builder(BuilderSettings builderSettings)
        {
            BuilderSettings = builderSettings ?? new BuilderSettings();
        }

        public ISingleObjectBuilder<T> CreateNew<T>()
        {
          
            var reflectionUtil = new ReflectionUtil();
            var propertyNamer = BuilderSettings.GetPropertyNamerFor<T>();
            return new ObjectBuilder<T>(reflectionUtil, BuilderSettings).WithPropertyNamer(propertyNamer);
        }

        public IListBuilder<T> CreateListOfSize<T>(int size)
        {
            Guard.Against(size < 1, "Size of list must be 1 or greater");
            var propertyNamer = BuilderSettings.GetPropertyNamerFor<T>();
            return CreateListOfSize<T>(size, propertyNamer);
        }

        public IListBuilder<T> CreateListOfSize<T>(int size, IPropertyNamer propertyNamer)
        {
            return new ListBuilder<T>(size, propertyNamer, new ReflectionUtil(),BuilderSettings);
        }
    }
}