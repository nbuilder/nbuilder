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
        private readonly IRandomGenerator<int> randomGenerator;
        private readonly IPropertyNamer<T> propertyNamer;
        private List<T> hierarchy;
        private int listCount;

        private Path path;

        public HierarchyGenerator(IList<T> initialList, Action<T, T> addMethod, int numberOfRoots, int depth, int min, int max, IRandomGenerator<int> randomGenerator, IPropertyNamer<T> propertyNamer)
        {
            this.initialList = initialList;
            this.action = addMethod;
            this.numberOfRoots = numberOfRoots;
            this.depth = depth;
            this.min = min;
            this.max = max;
            this.randomGenerator = randomGenerator;
            this.propertyNamer = propertyNamer;

            this.listCount = initialList.Count;

            int requiredSize = numberOfRoots*depth*max;

            if (listCount < requiredSize)
                throw new ArgumentException("The initial list must contain at least " + requiredSize + " items");
        }

        public HierarchyGenerator(IList<T> initialList, IHierarchySpec<T> spec, IRandomGenerator<int> randomGenerator, IPropertyNamer<T> propertyNamer)
            : this (initialList, spec.AddMethod, spec.NumberOfRoots, spec.Depth, spec.MinimumChildren, spec.MaximumChildren, randomGenerator, propertyNamer )
        {
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

                propertyNamer.SetValuesOf(item, sequenceNumber, path.ToString());

                hierarchy.Add(item);
                initialList.RemoveAt(0);
                listCount--;
            }

            for (int i = 0; i < numberOfRoots; i++)
            {
                path = new Path();
                path.SetCurrent(i + 1);
                AddChildren(hierarchy[i], 0);
            }

            return hierarchy;
        }

        private void AddChildren(T item, int currDepth)
        {
            int numberOfChildrenToAdd = randomGenerator.Generate(min, max);

            if (numberOfChildrenToAdd > 0)
                path.IncreaseDepth();

            for (int i = 0; i < numberOfChildrenToAdd; i++)
            {
                var child = initialList[0];
                initialList.RemoveAt(0);

                int sequenceNumber = i + 1;

                path.SetCurrent(sequenceNumber);
                propertyNamer.SetValuesOf(child, sequenceNumber, path.ToString());

                action(item, child);

                if (currDepth < (depth - 1))
                {
                    AddChildren(child, ++currDepth);
                } 
            }

            path.DecreaseDepth();
        }
    }
}