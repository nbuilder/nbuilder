//using FizzWare.NBuilder.PropertyValueNaming;

//namespace FizzWare.NBuilder
//{
//    public class ConstructorArgBuilder<T>
//    {
//        private readonly object[] constructorArgs;
        
//        public ConstructorArgBuilder(params object[] args)
//        {
//            this.constructorArgs = args;
//        }

//        //public SingleObjectBuilder<T> CreateNew()
//        //{
//        //    return CreateNew(new SequentialPropertyNameNamingStrategy<T>(new ReflectionUtil()));
//        //}

//        //public SingleObjectBuilder<T> CreateNew(INamingStrategy<T> strategy)
//        //{
//        //    return new SingleObjectBuilder<T>(strategy, new ReflectionUtil());
//        //}

//        //public SingleObjectBuilder<T> CreateNew(T prototype)
//        //{
//        //    return new SingleObjectBuilder<T>(prototype);
//        //}

//        public ListBuilder<T> CreateListOfSize(int size)
//        {
//            return CreateListOfSize(size, new SequentialPropertyNameNamingStrategy<T>(new ReflectionUtil()));
//        }

//        public ListBuilder<T> CreateListOfSize(int size, INamingStrategy<T> namingStrategy)
//        {
//            return new ListBuilder<T>(size, namingStrategy, new ReflectionUtil());
//        }

//        //public ListBuilder<T> CreateListOfSize(int size, T basedOn)
//        //{
//        //    return new ListBuilder<T>(size, basedOn);
//        //}
//    }
//}