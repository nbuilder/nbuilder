using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    [TestFixture]
    public class HierarchyBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            new SetupFixture().SetUp();
        }

        [Test]
        public void CreatingAHierarchyOfCategories()
        {
            var hierarchySpec = Builder<HierarchySpec<Category>>.CreateNew()
                .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
                .With(x => x.Depth = 5)
                .With(x => x.MaximumChildren = 10)
                .With(x => x.MinimumChildren = 5)
                .With(x => x.NamingMethod = (cat, title) => cat.Title = "Category " + title)
                .With(x => x.NumberOfRoots = 10).Build();

            Builder<Category>.CreateListOfSize(2500).PersistHierarchy(hierarchySpec);
        }
    }
}