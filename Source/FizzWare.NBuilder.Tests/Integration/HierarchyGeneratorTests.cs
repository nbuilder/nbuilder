using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    // //TODO: Finish

    //[TestFixture]
    //public class UsingHierarchyGenerator
    //{
    //    [Test]
    //    public void ShouldBeAbleToGenerateAHierarchy()
    //    {
    //        var hierarchySpec = Builder<HierarchySpec<MyHierarchicalClass>>.CreateNew()
    //            .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
    //            .With(x => x.Depth = 4)
    //            .With(x => x.MaximumChildren = 5)
    //            .With(x => x.MinimumChildren = 2)
    //            .With(x => x.NumberOfRoots = 10)
    //            .Build();

    //        var list = Builder<MyHierarchicalClass>
    //            .CreateListOfSize(1000)
    //            .BuildHierarchy(hierarchySpec);

    //        Assert.That(list.Count, Is.EqualTo(10));
    //    }
    //}
}