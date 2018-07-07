using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public class ListBuilder<T> : IListBuilderImpl<T>
    {
        private readonly IPropertyNamer propertyNamer;
        private readonly IReflectionUtil reflectionUtil;
        private readonly T[] mainList;
        private readonly DeclarationQueue<T> declarations;
        public BuilderSettings BuilderSettings { get; set; }

        public int Length => declarations.ListCapacity;

        public virtual int Capacity { get; }

        public IDeclarationQueue<T> Declarations => declarations;

        public ListBuilder(int size, IPropertyNamer propertyNamer, IReflectionUtil reflectionUtil, BuilderSettings builderSettings)
        {
            this.Capacity = size;
            this.propertyNamer = propertyNamer;
            this.reflectionUtil = reflectionUtil;
            BuilderSettings = builderSettings;

            mainList = new T[size];

            declarations = new DeclarationQueue<T>(size);

            ScopeUniqueRandomGenerator = new UniqueRandomGenerator();
        }

        public IObjectBuilder<T> CreateObjectBuilder()
        {
            return new ObjectBuilder<T>(reflectionUtil,this.BuilderSettings);
        }

        public IOperable<T> All()
        {
            declarations.Enqueue(new GlobalDeclaration<T>(this, CreateObjectBuilder()));
            return (IOperable<T>)declarations.GetLastItem();
        }

        public void Construct()
        {
            if (declarations.GetDistinctAffectedItemCount() < this.Capacity &&
                !declarations.ContainsGlobalDeclaration() &&
                reflectionUtil.RequiresConstructorArgs(typeof(T))
                )
            {
                throw new BuilderException(
                    @"The type requires constructor args but they have not be supplied for all the elements of the list");
            }

            if (declarations.GetDistinctAffectedItemCount() < this.Capacity && !declarations.ContainsGlobalDeclaration())
            {
                var globalDeclaration = new GlobalDeclaration<T>(this, CreateObjectBuilder());
                declarations.Enqueue(globalDeclaration);
            }

            declarations.Construct();
        }

        public IList<T> Name(IList<T> list)
        {
            if (!BuilderSettings.AutoNameProperties)
                return list;

            propertyNamer.SetValuesOfAllIn(list);
            return list;
        }

        public IList<T> Build()
        {
            Construct();
            declarations.AddToMaster(mainList);

            var list = mainList.ToList();

            Name(list);
            declarations.CallFunctions(list);

            return list;
        }

        public IDeclaration<T> AddDeclaration(IDeclaration<T> declaration)
        {
            this.declarations.Enqueue(declaration);
            return declarations.GetLastItem();
        }

        public IUniqueRandomGenerator ScopeUniqueRandomGenerator
        {
            get; private set;
        }
    }
}