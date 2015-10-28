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
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup).CreateListOfSize(10).TheFirst(11).With(x => x.StringOne = "Description").Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfTheNextRangeWillBeTooBig()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
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
            var builderSetup = new BuilderSetup();
            new Builder<IMyInterface>(builderSetup).CreateListOfSize(10).Build();
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void should_complain_if_you_try_to_create_an_abstract_class()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyAbstractClass>(builderSetup).CreateNew().Build();
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void should_complain_if_you_try_to_create_an_interface()
        {
            var builderSetup = new BuilderSetup();
            new Builder<IMyInterface>(builderSetup).CreateNew().Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfAndThePreviousRangeWillBeTooBig()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
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
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
                .CreateListOfSize(10)
                .Random(11)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfRandomAmountTooBigForRange()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
                 .CreateListOfSize(10)
                .Random(5, 0, 3)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfRandomRangeTooBig()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
                .CreateListOfSize(10)
                .Random(5, 0, 11)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfSizeOfListLessThanOne()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup).CreateListOfSize(0).Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfSectionGreaterThanListSize()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
                .CreateListOfSize(10)
                .Section(0, 10)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void CanOnlyUseAndTheNextAfterAnotherDeclaration()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
                .CreateListOfSize(10)
                .TheNext(5)
                    .With(x => x.StringOne = "test")
                .Build();
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void CanOnlyUseAndThePreviousAfterAnotherDeclaration()
        {
            var builderSetup = new BuilderSetup();
            new Builder<MyClass>(builderSetup)
                .CreateListOfSize(10)
                .ThePrevious(5)
                    .With(x => x.StringOne = "test")
                .Build();
        }
    }
}