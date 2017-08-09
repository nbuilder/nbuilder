using System.Collections.Generic;

namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyHierarchicalClass
    {
        public IList<MyHierarchicalClass> Children { get; set; }
        public MyHierarchicalClass Parent { get; set; }

        public string Title { get; set; }

        public MyHierarchicalClass()
        {
            Children = new List<MyHierarchicalClass>();
        }

        public void AddChild(MyHierarchicalClass child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
}