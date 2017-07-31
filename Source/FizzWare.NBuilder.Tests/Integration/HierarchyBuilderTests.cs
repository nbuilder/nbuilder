using FizzWare.NBuilder.Tests.Integration.Models;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class HierarchyBuilderTests
    {


        [Fact]
        public void CreatingAHierarchyOfCategoriesUsingRepositories()
        {
            var builderSetup = new RepositoryBuilderSetup().SetUp();
            const int depth = 3;
            const int minChildren = 3;
            const int maxChildren = 8;

            var builder = new Builder(builderSetup);
            var hierarchySpec = builder.CreateNew<HierarchySpec<Category>>()
                .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
                .With(x => x.Depth = depth)
                .With(x => x.MinimumChildren = minChildren)
                .With(x => x.MaximumChildren = maxChildren)
                .With(x => x.NamingMethod = (parent, child) => parent.Title = "Category " + child)
                .With(x => x.NumberOfRoots = 5)
                .Build();

            var categories = builder
                .CreateListOfSize<Category>(10000)
                .All()
                .PersistHierarchy(hierarchySpec);

            foreach (var root in categories)
            {
                root.Children.Count.ShouldBeGreaterThanOrEqualTo(minChildren);
                root.Children.Count.ShouldBeLessThanOrEqualTo(maxChildren);

                foreach (var child1 in root.Children)
                {
                    child1.Children.Count.ShouldBeGreaterThanOrEqualTo(minChildren);
                    child1.Children.Count.ShouldBeLessThanOrEqualTo(maxChildren);

                    foreach (var child2 in child1.Children)
                    {
                        child2.Children.Count.ShouldBeGreaterThanOrEqualTo(minChildren);
                        child2.Children.Count.ShouldBeLessThanOrEqualTo(maxChildren);
                    }
                }
            }
        }

        [Fact]
        public void CreatingAHierarchyOfCategoriesUsingEntityFramework()
        {
            var builderSetup = new PersistenceTestsBuilderSetup().SetUp();
            const int depth = 3;
            const int minChildren = 3;
            const int maxChildren = 8;

            var builder = new Builder(builderSetup);
            var hierarchySpec = builder.CreateNew< HierarchySpec<Category>>()
                .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
                .With(x => x.Depth = depth)
                .With(x => x.MinimumChildren = minChildren)
                .With(x => x.MaximumChildren = maxChildren)
                .With(x => x.NamingMethod = (parent, child) => parent.Title = "Category " + child)
                .With(x => x.NumberOfRoots = 5)
                .Build();

            var categories = builder
                .CreateListOfSize<Category>(10000)
                .All()
                .PersistHierarchy(hierarchySpec);

            foreach (var root in categories)
            {
                root.Children.Count.ShouldBeGreaterThanOrEqualTo(minChildren);
                root.Children.Count.ShouldBeLessThanOrEqualTo(maxChildren);

                foreach (var child1 in root.Children)
                {
                    child1.Children.Count.ShouldBeGreaterThanOrEqualTo(minChildren);
                    child1.Children.Count.ShouldBeLessThanOrEqualTo(maxChildren);

                    foreach (var child2 in child1.Children)
                    {
                        child2.Children.Count.ShouldBeGreaterThanOrEqualTo(minChildren);
                        child2.Children.Count.ShouldBeLessThanOrEqualTo(maxChildren);
                    }
                }
            }
        }

    }
}