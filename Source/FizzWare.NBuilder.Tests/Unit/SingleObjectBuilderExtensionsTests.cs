using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    [TestFixture]
    public class SingleObjectBuilderExtensionsTests
    {
        private ISingleObjectBuilder<MyClass> objectBuilder;
        private Func<MyClass, string> func;
        private Expression<Func<MyClass, int>> propertyExpression;
        private Action<MyClass> action;
        private Action<MyClass, SimpleClass> actionForAll;
        private IList<SimpleClass> actionList;

        [SetUp]
        public void SetUp()
        {
            objectBuilder = Substitute.For<IObjectBuilder<MyClass>>();
            func = x => x.StringOne = "test";
            propertyExpression = x => x.IntGetterOnly;
            action = x => x.DoSomething();
            actionForAll = (x, y) => x.Add(y);
            actionList = new List<SimpleClass>();
        }

        [Test]
        public void ShouldBeAbleToUseWith()
        {
            objectBuilder.With(func).Returns(objectBuilder);

            SingleObjectBuilderExtensions.With(objectBuilder, func);
        }

        [Test]
        public void ShouldBeAbleToUseWithConstructor()
        {
            Expression<Func<MyClass>> constructor = () => new MyClass();

            objectBuilder.WithConstructor(constructor).Returns(objectBuilder);

            SingleObjectBuilderExtensions.WithConstructor(objectBuilder, constructor);
        }

        [Test]
        public void ShouldBeAbleToUseAnd()
        {
            objectBuilder.With(func).Returns(objectBuilder);

            SingleObjectBuilderExtensions.And(objectBuilder, func);
        }

        [Test]
        public void ShouldBeAbleToUseAndToCallFunction()
        {
            objectBuilder.Do(action).Returns(objectBuilder);

            SingleObjectBuilderExtensions.And(objectBuilder, action);
        }

        [Test]
        public void ShouldBeAbleToAddMultiFunction()
        {
            objectBuilder.DoForAll(actionForAll, actionList).Returns(objectBuilder);

            SingleObjectBuilderExtensions.DoForAll(objectBuilder, actionForAll, actionList);
        }

        [Test]
        public void ShouldBeAbleToUseWithToSetPrivateProperties()
        {
            objectBuilder.Do(new Action<MyClass>(a => a.StringOne = "")).Returns(objectBuilder);

            SingleObjectBuilderExtensions.With(objectBuilder, propertyExpression, 100);
        }
    }
    // ReSharper restore InvokeAsExtensionMethod
}