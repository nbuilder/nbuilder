using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void ShouldBeAbleToPassListIntoHaveDoneToThem()
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
    }
    // ReSharper restore InvokeAsExtensionMethod
}