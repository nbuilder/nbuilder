using System;

namespace FizzWare.NBuilder
{
    public interface IHierarchySpec<T>
    {
        int Depth { get; set; }
        int NumberOfRoots { get; set; }
        int MinimumChildren { get; set; }
        int MaximumChildren { get; set; }
        Action<T, T> AddMethod { get; set; }
        Func<T, string, string> NamingMethod { get; set; }
    }
}