using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    [TestFixture]
    public class OperableExtensionTests
    {
        private MockRepository mocks;
        private IObjectBuilder<MyClass> objectBuilder;
        private Func<MyClass, float> func;
        private IDeclaration<MyClass> operable;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            objectBuilder = mocks.DynamicMock<IObjectBuilder<MyClass>>();
            operable = mocks.DynamicMultiMock<IDeclaration<MyClass>>(typeof(IOperable<MyClass>));
            func = x => x.Float = 1f;
        }

        [Test]
        public void ShouldBeAbleToUseHave()
        {
            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(new ObjectBuilder<MyClass>(null));
                objectBuilder.Expect(x => x.With(func));
            }

            OperableExtensions.Have((IOperable<MyClass>)operable, func);
        }

        [Test]
        public void ShouldBeAbleToUseHas()
        {
            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(new ObjectBuilder<MyClass>(null));
                objectBuilder.Expect(x => x.With(func));
            }

            OperableExtensions.Has((IOperable<MyClass>)operable, func);
        }

        [Test]
        public void ShouldBeAbleToUseAnd()
        {
            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(new ObjectBuilder<MyClass>(null));
                objectBuilder.Expect(x => x.With(func));
            }

            OperableExtensions.And((IOperable<MyClass>)operable, func);
        }

        [Test]
        public void ShouldBeAbleToUseHaveDoneToThemForAll()
        {
            var simpleClasses = new List<SimpleClass>();
            Action<MyClass, SimpleClass> action = (x, y) => x.Add(y);

            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(objectBuilder);
                objectBuilder.Expect(x => x.DoMultiple(action, simpleClasses)).Return(objectBuilder);
            }
            
            OperableExtensions.HaveDoneToThemForAll((IOperable<MyClass>)operable, action, simpleClasses);
        }

        [Test]
        public void ShouldBeAbleToUseHasDoneToItForAll()
        {
            var simpleClasses = new List<SimpleClass>();
            Action<MyClass, SimpleClass> action = (x, y) => x.Add(y);

            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(objectBuilder);
                objectBuilder.Expect(x => x.DoMultiple(action, simpleClasses)).Return(objectBuilder);
            }

            OperableExtensions.HasDoneToItForAll((IOperable<MyClass>)operable, action, simpleClasses);
        }

        [Test]
        public void ShouldBeAbleToUseHaveDoneToThem()
        {
            Action<MyClass> action = x => x.DoSomething();

            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(objectBuilder);
                objectBuilder.Expect(x => x.Do(action)).Return(objectBuilder);
            }

            OperableExtensions.HaveDoneToThem((IOperable<MyClass>)operable, action);
        }

        [Test]
        public void ShouldBeAbleToUseHasDoneToIt()
        {
            Action<MyClass> action = x => x.DoSomething();

            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(objectBuilder);
                objectBuilder.Expect(x => x.Do(action)).Return(objectBuilder);
            }

            OperableExtensions.HasDoneToIt((IOperable<MyClass>)operable, action);
        }

        [Test]
        public void ShouldBeAbleToUseAndToAddAnAction()
        {
            Action<MyClass> action = x => x.DoSomething();

            using (mocks.Record())
            {
                operable.Expect(x => x.ObjectBuilder).Return(objectBuilder);
                objectBuilder.Expect(x => x.Do(action)).Return(objectBuilder);
            }

            OperableExtensions.And((IOperable<MyClass>)operable, action);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfOperableIsNotAlsoOfTypeIDeclaration()
        {
            var operableOnly = mocks.DynamicMock<IOperable<MyClass>>();

            OperableExtensions.Have(operableOnly, x => x.StringOne = "test");
        }
    }
    // ReSharper restore InvokeAsExtensionMethod
}