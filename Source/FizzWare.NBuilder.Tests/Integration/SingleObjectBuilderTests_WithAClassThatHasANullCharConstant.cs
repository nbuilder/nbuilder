using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class SingleObjectBuilderTests_WithAClassThatHasANullCharConstant
    {
        [Test]
        public void ShouldBeAbleToCreateAClassThatHasANullCharConstant()
        {
            Builder<MyClassWithCharConst>.CreateNew().Build();

            Assert.That(MyClassWithCharConst.NullCharConst, Is.EqualTo('\0'));
        }

        [Test]
        public void ShouldBeAbleToCreateAClassThatHasACharConstant()
        {
            Builder<MyClassWithCharConst>.CreateNew().Build();

            Assert.That(MyClassWithCharConst.NonNullCharConst, Is.EqualTo('Y'));
        }
    }
}
