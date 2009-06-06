using System.Collections.Generic;
using Castle.ActiveRecord;
using System.Diagnostics;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    [ActiveRecord]
    [DebuggerDisplay("Id: {Id}, Title: {Title}, HasChildren: {HasChildren()}")]
    public class Category
    {
        [PrimaryKey(UnsavedValue = "0")]
        public int Id { get; set; }

        [Property]
        public string Title { get; set; }

        [Property]
        public string Description { get; set; }

        [HasMany(Table = "Category", ColumnKey = "ParentId", Cascade = ManyRelationCascadeEnum.SaveUpdate)]
        public IList<Category> Children { get; set; }

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