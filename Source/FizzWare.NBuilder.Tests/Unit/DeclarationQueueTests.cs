using System;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class DeclarationQueueTests
    {
        private DeclarationQueue<MyClass> declarations;
        private MockRepository mocks;
        private IDeclaration<MyClass> declaration1;
        private IDeclaration<MyClass> declaration2;
        private IGlobalDeclaration<MyClass> globalDeclaration;

        private const int listSize = 30;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            declarations = new DeclarationQueue<MyClass>(listSize);

            declaration1 = mocks.DynamicMock<IDeclaration<MyClass>>();
            declaration2 = mocks.DynamicMock<IDeclaration<MyClass>>();
            globalDeclaration = mocks.DynamicMock<IGlobalDeclaration<MyClass>>();

            using (mocks.Record())
            {
                declaration1.Stub(x => x.Start).Return(0).Repeat.Any();
                declaration1.Stub(x => x.End).Return(10).Repeat.Any();
            }
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowIfTryToDequeueWhenQueueEmpty()
        {
            declarations.Dequeue();
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
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfEndIsGreaterThanCapacity()
        {
            using (mocks.Record())
            {
                declaration1.Expect(x => x.Start).Return(0);
                declaration1.Expect(x => x.End).Return(9);

                declaration2.Expect(x => x.Start).Return(10);
                declaration2.Expect(x => x.End).Return(40);
            }

            using (mocks.Record())
            {
                declarations.Enqueue(declaration1);
                declarations.Enqueue(declaration2);
            }
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldComplainIfStartIsLessThanZero()
        {
            using (mocks.Record())
            {
                declaration1.BackToRecord(BackToRecordOptions.Expectations);

                declaration1.Expect(x => x.Start).Return(-2);
                declaration1.Expect(x => x.End).Return(9);
            }

            using (mocks.Record())
            {
                declarations.Enqueue(declaration1);
            }
        }
    }
}