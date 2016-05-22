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
       

        [Test]
        public void CreatingAHierarchyOfCategoriesUsingRepositories()
        {
            var builderSetup = new RepositoryBuilderSetup().SetUp();
            const int depth = 3;
            const int minChildren = 3;
            const int maxChildren = 8;

            var hierarchySpec = new Builder<HierarchySpec<Category>>(builderSetup).CreateNew()
                .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
                .With(x => x.Depth = depth)
                .With(x => x.MinimumChildren = minChildren)
                .With(x => x.MaximumChildren = maxChildren)
                .With(x => x.NamingMethod = (parent, child) => parent.Title = "Category " + child)
                .With(x => x.NumberOfRoots = 5)
                .Build();

            var categories = new Builder<Category>(builderSetup)
                .CreateListOfSize(10000)
                .All()
                .PersistHierarchy(hierarchySpec);

            foreach (var root in categories)
            {
                Assert.That(root.Children.Count, Is.AtLeast(minChildren));
                Assert.That(root.Children.Count, Is.AtMost(maxChildren));

                foreach (var child1 in root.Children)
                {
                    Assert.That(child1.Children.Count, Is.AtLeast(minChildren));
                    Assert.That(child1.Children.Count, Is.AtMost(maxChildren));

                    foreach (var child2 in child1.Children)
                    {
                        Assert.That(child2.Children.Count, Is.AtLeast(minChildren));
                        Assert.That(child2.Children.Count, Is.AtMost(maxChildren));
                    }
                }
            }
        }

        [Test]
        public void CreatingAHierarchyOfCategoriesUsingEntityFramework()
        {
            var builderSetup = new EntityFrameworkBuilderSetup().SetUp();
            const int depth = 3;
            const int minChildren = 3;
            const int maxChildren = 8;

            var hierarchySpec = new Builder<HierarchySpec<Category>>(builderSetup).CreateNew()
                .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
                .With(x => x.Depth = depth)
                .With(x => x.MinimumChildren = minChildren)
                .With(x => x.MaximumChildren = maxChildren)
                .With(x => x.NamingMethod = (parent, child) => parent.Title = "Category " + child)
                .With(x => x.NumberOfRoots = 5)
                .Build();

            var categories = new Builder<Category>(builderSetup)
                .CreateListOfSize(10000)
                .All()
                .PersistHierarchy(hierarchySpec);

            foreach (var root in categories)
            {
                Assert.That(root.Children.Count, Is.AtLeast(minChildren));
                Assert.That(root.Children.Count, Is.AtMost(maxChildren));

                foreach (var child1 in root.Children)
                {
                    Assert.That(child1.Children.Count, Is.AtLeast(minChildren));
                    Assert.That(child1.Children.Count, Is.AtMost(maxChildren));

                    foreach (var child2 in child1.Children)
                    {
                        Assert.That(child2.Children.Count, Is.AtLeast(minChildren));
                        Assert.That(child2.Children.Count, Is.AtMost(maxChildren));
                    }
                }
            }
        }

    }
}