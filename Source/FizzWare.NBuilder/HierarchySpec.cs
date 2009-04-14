using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FizzWare.NBuilder
{
    public class HierarchySpec<T> : IHierarchySpec<T>
    {
        public int Depth { get; set; }
        public int NumberOfRoots { get; set; }
        public int MinimumChildren { get; set; }
        public int MaximumChildren { get; set; }
        public Action<T, T> AddMethod { get; set; }
    }
}