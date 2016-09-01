using System;

namespace NBuilderCore
{
    public class HierarchySpec<T> : IHierarchySpec<T>
    {
        public int Depth { get; set; }
        public int NumberOfRoots { get; set; }
        public int MinimumChildren { get; set; }
        public int MaximumChildren { get; set; }
        public Action<T, T> AddMethod { get; set; }
        public Func<T, string, string> NamingMethod { get; set; }
    }
}