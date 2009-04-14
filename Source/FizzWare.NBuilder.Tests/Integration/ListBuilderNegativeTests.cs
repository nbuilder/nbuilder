using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using FizzWare.NBuilder.Tests.Unit;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class ListBuilderNegativeTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfWhereTheFirstRangeTooBig()
        {
            Builder<MyClass>.CreateListOfSize(10).WhereTheFirst(11).Have(x => x.StringOne = "Description").Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfAndTheNextRangeWillBeTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .WhereTheFirst(5)
                    .Have(x => x.StringOne = "Description")
                .AndTheNext(10)
                    .Have(x => x.StringOne = "Description2")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfYouTryToCreateAnInterface()
        {
            Builder<IMyClass>.CreateListOfSize(10).Build();
        }
    }
}