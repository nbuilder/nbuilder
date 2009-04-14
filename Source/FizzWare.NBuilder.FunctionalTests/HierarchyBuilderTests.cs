using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    // TODO: Hierarchy builder is not supported yet.
    // TODO: It will be included in next version
    //[TestFixture]
    //public class HierarchyBuilderTests
    //{
    //    [TestFixtureSetUp]
    //    public void TestFixtureSetUp()
    //    {
    //        new SetupFixture().TestFixtureSetUp();
    //    }

    //    [Test]
    //    public void CreatingAHierarchyOfCategories()
    //    {
    //        var hierarchySpec = Builder<HierarchySpec<Category>>.CreateNew()
    //            .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
    //            .With(x => x.Depth = 2)
    //            .With(x => x.MaximumChildren = 1)
    //            .With(x => x.MinimumChildren = 1)
    //            .With(x => x.NumberOfRoots = 4).Build();

    //        //Builder<Category>.CreateListOfSize(1000)
    //        //    .WhereAll()
    //        //        .Have(x => x.Id = 0)
    //        //    .PersistHierarchy(hierarchySpec);

    //        Builder<Category>.CreateListOfSize(60).WhereAll().Have(x => x.Title = null)
    //            .PersistHierarchy(hierarchySpec);
    //    }
    //}
}