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
        public void ShouldComplainIfTheFirstRangeTooBig()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var builderSetup = new BuilderSettings();
                new Builder(builderSetup).CreateListOfSize< MyClass>(10).TheFirst(11).With(x => x.StringOne = "Description").Build();
            });
        }

        [Test]
        public void ShouldComplainIfTheNextRangeWillBeTooBig()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<BuilderException>(() =>
            {
                new Builder(builderSetup)
                     .CreateListOfSize< MyClass>(10)
                    .TheFirst(5)
                        .With(x => x.StringOne = "Description")
                    .TheNext(10)
                        .With(x => x.StringOne = "Description2")
                    .Build();

            });
        }

        [Test]
        public void ShouldComplainIfYouTryToCreateAnInterface()
        {
            var builderSetup = new BuilderSettings();
            Assert.Throws<BuilderException>(() =>
            {
                new Builder(builderSetup).CreateListOfSize< IMyInterface>(10).Build();
            });
        }

        [Test]
        public void should_complain_if_you_try_to_create_an_abstract_class()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<TypeCreationException>(() =>
            {
                new Builder(builderSetup).CreateNew< MyAbstractClass>().Build();
            });
        }

        [Test]
        public void should_complain_if_you_try_to_create_an_interface()
        {
            var builderSetup = new BuilderSettings();
            Assert.Throws<TypeCreationException>(() =>
            {
                new Builder(builderSetup).CreateNew< IMyInterface>().Build();
            });
        }

        [Test]
        public void ShouldComplainIfAndThePreviousRangeWillBeTooBig()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<BuilderException>(() =>
            {
                new Builder(builderSetup)
                     .CreateListOfSize< MyClass>(10)
                    .TheLast(5)
                        .With(x => x.StringOne = "test")
                    .ThePrevious(6)
                        .With(x => x.Int = 2)
                    .Build();

            });
        }

        [Test]
        public void ShouldComplainIfRandomAmountTooBig()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<ArgumentException>(() =>
            {
                new Builder(builderSetup)
                    .CreateListOfSize< MyClass>(10)
                    .Random(11)
                    .With(x => x.StringOne = "test")
                    .Build();
            });
        }

        [Test]
        public void ShouldComplainIfRandomAmountTooBigForRange()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<ArgumentException>(() =>
            {
                new Builder(builderSetup)
                     .CreateListOfSize< MyClass>(10)
                    .Random(5, 0, 3)
                        .With(x => x.StringOne = "test")
                    .Build();
            });
        }

        [Test]
        public void ShouldComplainIfRandomRangeTooBig()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<BuilderException>(() =>
            {
                new Builder(builderSetup)
                    .CreateListOfSize< MyClass>(10)
                    .Random(5, 0, 11)
                        .With(x => x.StringOne = "test")
                    .Build();
            });
        }

        [Test]
        public void ShouldComplainIfSizeOfListLessThanOne()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<ArgumentException>(() =>
            {
                new Builder(builderSetup).CreateListOfSize< MyClass>(0).Build();
            });
        }

        [Test]
        public void ShouldComplainIfSectionGreaterThanListSize()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<ArgumentException>(() =>
            {
                new Builder(builderSetup)
                    .CreateListOfSize< MyClass>(10)
                    .Section(0, 10)
                        .With(x => x.StringOne = "test")
                    .Build();
            });
        }

        [Test]
        public void CanOnlyUseAndTheNextAfterAnotherDeclaration()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<BuilderException>(() =>
            {
                new Builder(builderSetup)
                    .CreateListOfSize< MyClass>(10)
                    .TheNext(5)
                        .With(x => x.StringOne = "test")
                    .Build();
            });
        }

        [Test]
        public void CanOnlyUseAndThePreviousAfterAnotherDeclaration()
        {
            var builderSetup = new BuilderSettings();

            Assert.Throws<BuilderException>(() =>
            {
                new Builder(builderSetup)
                    .CreateListOfSize< MyClass>(10)
                    .ThePrevious(5)
                        .With(x => x.StringOne = "test")
                    .Build();
            });
        }
    }
}