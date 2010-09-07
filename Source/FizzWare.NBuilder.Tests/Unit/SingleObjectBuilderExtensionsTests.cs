using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    [TestFixture]
    public class SingleObjectBuilderExtensionsTests
    {
        private ISingleObjectBuilder<MyClass> objectBuilder;
        private MockRepository mocks;
        private Func<MyClass, string> func;
        private Action<MyClass> action;
        private Action<MyClass, SimpleClass> actionForAll;
        private IList<SimpleClass> actionList;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            objectBuilder = mocks.DynamicMock<IObjectBuilder<MyClass>>();
            func = x => x.StringOne = "test";
            action = x => x.DoSomething();
            actionForAll = (x,y) => x.Add(y);
            actionList = new List<SimpleClass>();
        }

        [Test]
        public void ShouldBeAbleToUseWith()
        {
            using (mocks.Record())
                objectBuilder.Expect(x => x.With(func)).Return(objectBuilder);

            using (mocks.Playback())
                SingleObjectBuilderExtensions.With(objectBuilder, func);
        }

        [Test]
        public void ShouldBeAbleToUseWithConstructor()
        {
            Expression<Func<MyClass>> constructor = () => new MyClass();

            using (mocks.Record())
                objectBuilder.Expect(x => x.WithConstructor(constructor)).Return(objectBuilder);

            using (mocks.Playback())
                SingleObjectBuilderExtensions.WithConstructor(objectBuilder, constructor);
        }

        [Test]
        public void ShouldBeAbleToUseAnd()
        {
            using (mocks.Record())
                objectBuilder.Expect(x => x.With(func)).Return(objectBuilder);

            using (mocks.Playback())
                SingleObjectBuilderExtensions.And(objectBuilder, func);
        }

        [Test]
        public void ShouldBeAbleToUseAndToCallFunction()
        {
            using (mocks.Record())
                objectBuilder.Expect(x => x.Do(action)).Return(objectBuilder);

            using (mocks.Playback())
                SingleObjectBuilderExtensions.And(objectBuilder, action);
        }

        [Test]
        public void ShouldBeAbleToAddMultiFunction()
        {
            using (mocks.Record())
                objectBuilder.Expect(x => x.DoForAll(actionForAll, actionList)).Return(objectBuilder);

            using (mocks.Playback())
                SingleObjectBuilderExtensions.DoForAll(objectBuilder, actionForAll, actionList);
        }
    }
    // ReSharper restore InvokeAsExtensionMethod
}