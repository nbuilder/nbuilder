using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
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
            Builder<IMyInterface>.CreateListOfSize(10).Build();
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void should_complain_if_you_try_to_create_an_abstract_class()
        {
            Builder<MyAbstractClass>.CreateNew().Build();
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void should_complain_if_you_try_to_create_an_interface()
        {
            Builder<IMyInterface>.CreateNew().Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfAndThePreviousRangeWillBeTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .WhereTheLast(5)
                    .Have(x => x.StringOne = "test")
                .AndThePrevious(6)
                    .Have(x => x.Int = 2)
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfWhereRandomAmountTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .WhereRandom(11)
                    .Have(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfWhereRandomAmountTooBigForRange()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .WhereRandom(5, 0, 3)
                    .Have(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfWhereRandomRangeTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .WhereRandom(5, 0, 11)
                    .Have(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfSizeOfListLessThanOne()
        {
            Builder<MyClass>.CreateListOfSize(0).Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfSectionGreaterThanListSize()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .WhereSection(0, 10)
                    .Have(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void CanOnlyUseAndTheNextAfterAnotherDeclaration()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .AndTheNext(5)
                    .Have(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void CanOnlyUseAndThePreviousAfterAnotherDeclaration()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .AndThePrevious(5)
                    .Have(x => x.StringOne = "test")
                .Build();
        }
    }
}