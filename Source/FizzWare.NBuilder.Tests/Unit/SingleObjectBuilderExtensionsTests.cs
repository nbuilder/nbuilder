using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    
    public class SingleObjectBuilderExtensionsTests
    {
        private ISingleObjectBuilder<MyClass> objectBuilder;
        private Func<MyClass, string> func;
        private Expression<Func<MyClass, int>> propertyExpression;
        private Action<MyClass> action;
        private Action<MyClass, SimpleClass> actionForAll;
        private IList<SimpleClass> actionList;

        public SingleObjectBuilderExtensionsTests()
        {
            objectBuilder = Substitute.For<IObjectBuilder<MyClass>>();
            func = x => x.StringOne = "test";
            propertyExpression = x => x.IntGetterOnly;
            action = x => x.DoSomething();
            actionForAll = (x, y) => x.Add(y);
            actionList = new List<SimpleClass>();
        }

        [Fact]
        public void ShouldBeAbleToUseWith()
        {
            objectBuilder.With(func).Returns(objectBuilder);

            SingleObjectBuilderExtensions.With(objectBuilder, func);
        }

        [Fact]
        public void ShouldBeAbleToUseWithConstructor()
        {
            Expression<Func<MyClass>> constructor = () => new MyClass();

            objectBuilder.WithConstructor(constructor).Returns(objectBuilder);

            SingleObjectBuilderExtensions.WithConstructor(objectBuilder, constructor);
        }

        [Fact]
        public void ShouldBeAbleToUseAnd()
        {
            objectBuilder.With(func).Returns(objectBuilder);

            SingleObjectBuilderExtensions.And(objectBuilder, func);
        }

        [Fact]
        public void ShouldBeAbleToUseAndToCallFunction()
        {
            objectBuilder.Do(action).Returns(objectBuilder);

            SingleObjectBuilderExtensions.And(objectBuilder, action);
        }

        [Fact]
        public void ShouldBeAbleToAddMultiFunction()
        {
            objectBuilder.DoForAll(actionForAll, actionList).Returns(objectBuilder);

            SingleObjectBuilderExtensions.DoForAll(objectBuilder, actionForAll, actionList);
        }

        [Fact]
        public void ShouldBeAbleToUseWithToSetPrivateProperties()
        {
            objectBuilder.Do(new Action<MyClass>(a => a.StringOne = "")).Returns(objectBuilder);

            SingleObjectBuilderExtensions.With(objectBuilder, propertyExpression, 100);
        }
    }
    // ReSharper restore InvokeAsExtensionMethod
}