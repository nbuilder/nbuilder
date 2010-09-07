using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ExtensibilityTests
    {
        private const string theString = "test";

        [Test]
        public void ShouldBeAbleToAddCustomWhereExtension()
        {
            var list = Builder<MyClass>.CreateListOfSize(10).WhereAllEven().Have(x => x.StringOne = theString).Build();
            Assert.That(list.Count(x => x.StringOne == theString), Is.EqualTo(5));
        }
    }
}