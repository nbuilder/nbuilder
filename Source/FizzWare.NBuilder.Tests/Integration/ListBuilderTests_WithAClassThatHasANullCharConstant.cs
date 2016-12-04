using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class ListBuilderBuilderTests_WithAClassThatHasANullCharConstant
    {
        [Test]
        public void ShouldBeAbleToCreateAListOfAClassThatHasANullCharConstant()
        {
            var builderSetup = new BuilderSettings();
            var list = new Builder(builderSetup).CreateListOfSize< MyClassWithCharConst>(2).Build();

            foreach (var item in list)
            {
                Assert.That(item.GetNullCharConst(), Is.EqualTo(MyClassWithCharConst.NullCharConst));
            }           
        }

        [Test]
        public void ShouldBeAbleToCreateAListOfAClassThatHasACharConstant()
        {
            var builderSetup = new BuilderSettings();
            var list = new Builder(builderSetup).CreateListOfSize< MyClassWithCharConst>(2).Build();

            foreach (var item in list)
            {
                Assert.That(item.GetNonNullCharConst(), Is.EqualTo(MyClassWithCharConst.NonNullCharConst));
            }            
        }
    }
}
