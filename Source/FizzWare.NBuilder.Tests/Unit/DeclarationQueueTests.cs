using System;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class DeclarationQueueTests
    {
        private DeclarationQueue<MyClass> declarations;
        private IDeclaration<MyClass> declaration1;
        private IDeclaration<MyClass> declaration2;
        private IGlobalDeclaration<MyClass> globalDeclaration;

        private const int listSize = 30;

        [SetUp]
        public void SetUp()
        {
            declarations = new DeclarationQueue<MyClass>(listSize);

            declaration1 = Substitute.For<IDeclaration<MyClass>>();
            declaration2 = Substitute.For<IDeclaration<MyClass>>();
            globalDeclaration = Substitute.For<IGlobalDeclaration<MyClass>>();

            declaration1.Start.Returns(0);
            declaration1.End.Returns(10);
        }

        [Test]
        public void CountShouldIncreaseWhenAddingAnItem()
        {
            declarations.Enqueue(declaration1);
            Assert.That(declarations.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldBeAbleToDequeueAnItem()
        {
            declarations.Enqueue(declaration1);
            Assert.That(declarations.Dequeue(), Is.SameAs(declaration1));
        }

        [Test]
        public void CountShouldDecreaseWhenDequeuingAnItem()
        {
            declarations.Enqueue(declaration1);
            declarations.Dequeue();
            Assert.That(declarations.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldThrowIfTryToDequeueWhenQueueEmpty()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                declarations.Dequeue();
            });
        }

        [Test]
        public void FirstItemsInShouldComeOutFirst()
        {
            declarations.Enqueue(declaration1);
            declarations.Enqueue(globalDeclaration);

            Assert.That(declarations.Dequeue(), Is.SameAs(declaration1));
            Assert.That(declarations.Dequeue(), Is.SameAs(globalDeclaration));
        }

        [Test]
        public void GlobalDeclarationsShouldBePrioritised()
        {
            declarations.Enqueue(declaration1);
            declarations.Enqueue(globalDeclaration);

            declarations.Prioritise();

            Assert.That(declarations.Dequeue(), Is.SameAs(globalDeclaration));
            Assert.That(declarations.Dequeue(), Is.SameAs(declaration1));
            Assert.That(declarations.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToPrioritiseMultipleGlobalDeclarations()
        {
            declarations.Enqueue(declaration1);
            declarations.Enqueue(globalDeclaration);
            declarations.Enqueue(globalDeclaration);

            declarations.Prioritise();

            Assert.That(declarations.Dequeue(), Is.SameAs(globalDeclaration));
            Assert.That(declarations.Dequeue(), Is.SameAs(globalDeclaration));
            Assert.That(declarations.Dequeue(), Is.SameAs(declaration1));
        }

        [Test]
        public void ShouldBeAbleToPeek()
        {
            declarations.Enqueue(declaration1);
            Assert.That(declarations.GetLastItem(), Is.SameAs(declaration1));
            Assert.That(declarations.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldComplainIfEndIsGreaterThanCapacity()
        {
            declaration1.Start.Returns(0);
            declaration1.End.Returns(9);

            declaration2.Start.Returns(10);
            declaration2.End.Returns(40);

            declarations.Enqueue(declaration1);

            Assert.Throws<BuilderException>(() =>
            {
                declarations.Enqueue(declaration2);
            });

        }

        [Test]
        public void ShouldComplainIfStartIsLessThanZero()
        {
            //declaration1.BackToRecord(BackToRecordOptions.Expectations);

            declaration1.Start.Returns(-2);
            declaration1.End.Returns(9);

            Assert.Throws<BuilderException>(() =>
                {
                    declarations.Enqueue(declaration1);
                });

        }
    }
}