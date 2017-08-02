using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class ListBuilderExtensions
    {
        public static IOperable<T> TheFirst<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);

            Guard.Against(amount < 1, "TheFirst amount must be 1 or greater");
            Guard.Against(amount > listBuilderImpl.Capacity, "TheFirst amount must be less than the size of the list that is being generated");
            
            var declaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), 0, amount - 1);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }


        public static IOperable<T> TheLast<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);

            // TODO: Put these in a specification

            Guard.Against(amount < 1, "TheLast amount must be 1 or greater");
            Guard.Against(amount > listBuilderImpl.Capacity, "TheLast amount must be less than the size of the list that is being generated");

            int start = listBuilderImpl.Capacity - amount;
            var declaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), start, listBuilderImpl.Capacity - 1);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }

        public static IOperable<T> Random<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            return Random(listBuilderImpl, amount, 0, listBuilderImpl.Capacity);
        }

        public static IOperable<T> Random<T>(this IListBuilder<T> listBuilder, int amount, int start, int end)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);

            // TODO: Put these in a specification
            Guard.Against(amount < 1, "Random amount must be 1 or greater");
            Guard.Against(amount > listBuilderImpl.Capacity, "Random amount must be less than the size of the list that is being generated");

            var declaration = new RandomDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), listBuilderImpl.ScopeUniqueRandomGenerator, amount, start, end);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }


        public static IOperable<T> Section<T>(this IListBuilder<T> listBuilder, int start, int end)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            var capacity = listBuilderImpl.Capacity;

            // TODO: Put these in a specification
            Guard.Against(start < 0, "Section - start must be zero or greater");
            Guard.Against(start >= capacity, "Section - start must be less than the capacity");

            Guard.Against(end < 1, "Section - end must be greater than one");
            Guard.Against(end >= capacity, "Section - end must be less than the capacity");

            Guard.Against(start >= end, "Section - end must be greater than start");

            var declaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(), start, end);
            return (IOperable<T>)listBuilderImpl.AddDeclaration(declaration);
        }


        public static IOperable<T> TheNext<T>(this IListBuilder<T> listBuilder, int amount)
        {
            // TODO: Put this in a specification
            Guard.Against(amount < 1, "TheNext - amount must be one or greater");

            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            var lastDeclaration = listBuilderImpl.Declarations.GetLastItem();
            var rangeDeclaration = lastDeclaration as RangeDeclaration<T>;

            if (rangeDeclaration == null)
                throw new BuilderException("Before using TheNext you must have just used a RangeDeclaration - i.e. (TheFirst or Section)");

            int start = rangeDeclaration.End + 1;
            int end = start + amount - 1;

            var andTheNextDeclaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(),
                                                                start, end);

            listBuilderImpl.AddDeclaration(andTheNextDeclaration);
            return andTheNextDeclaration;
        }

        public static IOperable<T> ThePrevious<T>(this IListBuilder<T> listBuilder, int amount)
        {
            var listBuilderImpl = GetListBuilderImpl<T>(listBuilder);
            var lastDeclaration = listBuilderImpl.Declarations.GetLastItem();

            var rangeDeclaration = lastDeclaration as RangeDeclaration<T>;

            if (rangeDeclaration == null)
                throw new BuilderException("Before using ThePrevious you must have just used a RangeDeclaration - i.e. (TheFirst or Section)");

            int start = rangeDeclaration.Start - amount;
            int end = start + amount - 1;

            var andTheNextDeclaration = new RangeDeclaration<T>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder(),
                                                                start, end);

            listBuilderImpl.AddDeclaration(andTheNextDeclaration);
            return andTheNextDeclaration;
        }


        public static IList<T> BuildHierarchy<T>(this IListBuilder<T> listBuilder, IHierarchySpec<T> hierarchySpec)
        {
            var list = listBuilder.Build();

            var hierarchy = new HierarchyGenerator<T>(list, hierarchySpec.AddMethod, hierarchySpec.NumberOfRoots, hierarchySpec.Depth,
                                      hierarchySpec.MinimumChildren, hierarchySpec.MaximumChildren,
                                      new RandomGenerator(), hierarchySpec.NamingMethod, null).Generate();

            return hierarchy;
        }

        public static IList<T> PersistHierarchy<T>(this IListBuilder<T> listBuilder, IHierarchySpec<T> hierarchySpec)
        {
            // 1. Create
            var list = listBuilder.Build();

            // 2. Reorganise
            var hierarchy = new HierarchyGenerator<T>(list, hierarchySpec.AddMethod, hierarchySpec.NumberOfRoots, hierarchySpec.Depth,
                                      hierarchySpec.MinimumChildren, hierarchySpec.MaximumChildren,
                                      new RandomGenerator(), hierarchySpec.NamingMethod, listBuilder.BuilderSettings.GetPersistenceService()).Generate();

            return hierarchy;
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
    }
}