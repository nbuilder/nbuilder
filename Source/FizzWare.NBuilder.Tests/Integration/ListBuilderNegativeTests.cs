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
        public void ShouldComplainIfTheFirstRangeTooBig()
        {
            Builder<MyClass>.CreateListOfSize(10).TheFirst(11).With(x => x.StringOne = "Description").Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfTheNextRangeWillBeTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .TheFirst(5)
                    .With(x => x.StringOne = "Description")
                .TheNext(10)
                    .With(x => x.StringOne = "Description2")
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
                .TheLast(5)
                    .With(x => x.StringOne = "test")
                .ThePrevious(6)
                    .With(x => x.Int = 2)
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfRandomAmountTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .Random(11)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfRandomAmountTooBigForRange()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .Random(5, 0, 3)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfRandomRangeTooBig()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .Random(5, 0, 11)
                    .With(x => x.StringOne = "test")
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
                .Section(0, 10)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void CanOnlyUseAndTheNextAfterAnotherDeclaration()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .TheNext(5)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void CanOnlyUseAndThePreviousAfterAnotherDeclaration()
        {
            Builder<MyClass>
                .CreateListOfSize(10)
                .ThePrevious(5)
                    .With(x => x.StringOne = "test")
                .Build();
        }
    }
}