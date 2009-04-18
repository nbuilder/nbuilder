using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public class HierarchyGenerator<T>
    {
        private readonly IList<T> initialList;
        private readonly Action<T, T> action;
        private readonly int numberOfRoots;
        private readonly int depth;
        private readonly int min;
        private readonly int max;
        private readonly IRandomGenerator randomGenerator;
        private readonly Func<T, string, string> namingMethod;
        private readonly IPersistenceService persistenceService;
        private List<T> hierarchy;
        private int listCount;

        private Path path;

        public HierarchyGenerator(IList<T> initialList, Action<T, T> addMethod, int numberOfRoots, int depth, int min, int max, IRandomGenerator randomGenerator, Func<T, string, string> namingMethod, IPersistenceService persistenceService)
        {
            this.initialList = initialList;
            this.action = addMethod;
            this.numberOfRoots = numberOfRoots;
            this.depth = depth;
            this.min = min;
            this.max = max;
            this.randomGenerator = randomGenerator;
            this.namingMethod = namingMethod;
            this.persistenceService = persistenceService;

            this.listCount = initialList.Count;

            int requiredSize = numberOfRoots * depth * (depth * max);

            if (listCount < requiredSize)
                throw new ArgumentException("The initial list must contain at least " + requiredSize + " items");
        }

        public IList<T> Generate()
        {
            hierarchy = new List<T>();

            path = new Path();

            for (int i = 0; i < numberOfRoots; i++)
            {
                var item = initialList[0];

                int sequenceNumber = i + 1;

                path.SetCurrent(sequenceNumber);

                if (namingMethod != null)
                    namingMethod(item, path.ToString());

                hierarchy.Add(item);

                TryPersistCreate(item);

                initialList.RemoveAt(0);
                listCount--;
            }

            for (int i = 0; i < numberOfRoots; i++)
            {
                path = new Path();
                path.SetCurrent(i + 1);
                AddChildren(hierarchy[i], 0);
            }

            TryPersistUpdateAll((IList<T>)hierarchy);

            return hierarchy;
        }

        private void TryPersistUpdateAll(IList<T> list)
        {
            if (persistenceService != null)
                persistenceService.Update(list);
        }

        private void TryPersistCreate(T item)
        {
            if (persistenceService != null)
                persistenceService.Create(item);
        }

        private void AddChildren(T item, int currDepth)
        {
            int numberOfChildrenToAdd = randomGenerator.Next(min, max);

            if (numberOfChildrenToAdd > 0)
                path.IncreaseDepth();

            for (int i = 0; i < numberOfChildrenToAdd; i++)
            {
                var child = initialList[0];
                initialList.RemoveAt(0);

                int sequenceNumber = i + 1;

                path.SetCurrent(sequenceNumber);

                if  (namingMethod != null)
                    namingMethod(child, path.ToString());

                action(item, child);

                TryPersistCreate(child);

                if (currDepth < (depth - 1))
                {
                    AddChildren(child, ++currDepth);
                } 
            }

            path.DecreaseDepth();
        }
    }
}