using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    public static class ListBuilderExtensions
    {
        public static IOperable<T> WhereTheFirst<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);

            Guard.Against(amount < 1, "WhereTheFirst amount must be 1 or greater");
            Guard.Against(amount > listBuilderImpl.Capacity, "WhereTheFirst amount must be less than the size of the list that is being generated");
            
            var declaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), 0, amount - 1);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }

        public static IOperable<T> WhereTheLast<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);

            Guard.Against(amount < 1, "WhereTheLast amount must be 1 or greater");
            Guard.Against(amount > listBuilderImpl.Capacity, "WhereTheLast amount must be less than the size of the list that is being generated");

            int start = listBuilderImpl.Capacity - amount;
            var declaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), start, listBuilderImpl.Capacity - 1);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }

        public static IOperable<T> WhereRandom<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);

            Guard.Against(amount < 1, "WhereRandom amount must be 1 or greater");
            Guard.Against(amount > listBuilderImpl.Capacity, "WhereRandom amount must be less than the size of the list that is being generated");

            var declaration = new RandomDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), new UniqueRandomGenerator<int>(), amount);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }

        public static IOperable<T> WhereSection<T>(this IListBuilder<T> listBuilder, int start, int end)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            var capacity = listBuilderImpl.Capacity;

            Guard.Against(start < 0, "WhereSection - start must be zero or greater");
            Guard.Against(start >= capacity, "WhereSection - start must be less than the capacity");

            Guard.Against(end < 1, "WhereSection - end must be greater than one");
            Guard.Against(end >= capacity, "WhereSection - end must be less than the capacity");

            Guard.Against(start >= end, "WhereSection - end must be greater than start");

            var declaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), start, end);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }

        public static IOperable<T> AndTheNext<T>(this IListBuilder<T> listBuilder, int amount)
        {
            Guard.Against(amount < 1, "AndTheNext - amount must be one or greater");

            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            var lastDeclaration = listBuilderImpl.Declarations.Peek();
            var rangeDeclaration = lastDeclaration as RangeDeclaration<T>;

            if (rangeDeclaration == null)
                throw new BuilderException("Before using AndTheNext you must have just used a RangeDeclaration - i.e. (WhereTheFirst or WhereSection)");

            int start = rangeDeclaration.End + 1;
            int end = start + amount - 1;

            var andTheNextDeclaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(),
                                                                start, end);

            listBuilderImpl.AddDeclaration(andTheNextDeclaration);
            return andTheNextDeclaration;
        }

        public static IOperable<T> AndThePrevious<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            var lastDeclaration = listBuilderImpl.Declarations.Peek();

            var rangeDeclaration = lastDeclaration as RangeDeclaration<T>;

            if (rangeDeclaration == null)
                throw new BuilderException("Before using AndThePrevious you must have just used a RangeDeclaration - i.e. (WhereTheFirst or WhereSection)");

            int start = rangeDeclaration.Start - amount;
            int end = start + amount - 1;

            var andTheNextDeclaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(),
                                                                start, end);

            listBuilderImpl.AddDeclaration(andTheNextDeclaration);
            return andTheNextDeclaration;
        }

        private static IListBuilderImpl<T> GetListBuilderImpl<T>(object obj)
        {
            var listBuilderImpl = obj as IListBuilderImpl<T>;

            if (listBuilderImpl != null)
                return listBuilderImpl;

            var decl = obj as IDeclaration<T>;

            if (decl != null)
                listBuilderImpl = decl.ListBuilderImpl;

            return listBuilderImpl;
        }

        public static IList<T> BuildHierarchy<T>(this IListBuilder<T> listBuilder, IHierarchySpec<T> hierarchySpec)
        {
            HierarchyGenerator<T> generator = new HierarchyGenerator<T>(listBuilder.Build(), hierarchySpec, new RandomGenerator<int>(), new SequentialPropertyNamer<T>(new ReflectionUtil()));
            return generator.Generate();
        }

        public static IList<T> PersistHierarchy<T>(this IListBuilder<T> listBuilder, IHierarchySpec<T> hierarchySpec)
        {
            var list = BuildHierarchy(listBuilder, hierarchySpec);

            var persistenceService = BuilderSetup.GetPersistenceService();
            persistenceService.Persist(list);
            return list;
        }
    }
}