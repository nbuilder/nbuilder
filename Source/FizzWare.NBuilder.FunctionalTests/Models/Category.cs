using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    
    [DebuggerDisplay("Id: {Id}, Title: {Title}, HasChildren: {HasChildren()}")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual List<Category> Children { get; set; }

        public virtual List<Product> Products { get; set; } 

        public Category()
        {
            Children = new List<Category>();
        }

        public void AddChild(Category child)
        {
            this.Children.Add(child);
        }

        public void AddChildren(IList<Category> children)
        {
            for (int i = 0; i < children.Count; i++)
            {
                this.Children.Add(children[i]);
            }
        }

        public bool HasChildren()
        {
            return Children.Count > 0;
        }
    }
}