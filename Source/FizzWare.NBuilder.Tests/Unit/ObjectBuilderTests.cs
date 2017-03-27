using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ObjectBuilderTests
    {
        private IReflectionUtil reflectionUtil;
        private ObjectBuilder<MyClass> builder;
        private ObjectBuilder<MyClassWithConstructor> myClassWithConstructorBuilder;
        private ObjectBuilder<MyClassWithOptionalConstructor> myClassWithOptionalConstructorBuilder;

        [SetUp]
        public void SetUp()
        {
            var builderSetup = new BuilderSettings();

            reflectionUtil = Substitute.For<IReflectionUtil>();

            builder = new ObjectBuilder<MyClass>(reflectionUtil, builderSetup);
            myClassWithConstructorBuilder = new ObjectBuilder<MyClassWithConstructor>(reflectionUtil, builderSetup);
            myClassWithOptionalConstructorBuilder = new ObjectBuilder<MyClassWithOptionalConstructor>(reflectionUtil, builderSetup);
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithNoConstructorArgs()
        {

            reflectionUtil.RequiresConstructorArgs(typeof(MyClass)).Returns(false);
            reflectionUtil.CreateInstanceOf<MyClass>().Returns(new MyClass());

            builder.Construct(index: 1);
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithConstructorArgs()
        {
            const int arg1 = 1;
            const float arg2 = 2f;


            {
                reflectionUtil.RequiresConstructorArgs(typeof(MyClassWithConstructor)).Returns(true);
                reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(arg1, arg2).Returns(new MyClassWithConstructor(arg1, arg2));
            }


            {
#pragma warning disable 0618
                myClassWithConstructorBuilder
                        .WithConstructor(() => new MyClassWithConstructor(arg1, arg2))
                        .Construct(1);
#pragma warning restore 0618
            }
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithNullableConstructorArgs_using_expression_syntax()
        {
            const string arg1 = null;


            {
                reflectionUtil.RequiresConstructorArgs(typeof(MyClassWithConstructor)).Returns(true);
                reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(arg1).Returns(new MyClassWithConstructor(arg1));
            }


            {
                myClassWithConstructorBuilder
                        .WithConstructor(() => new MyClassWithConstructor(arg1))
                        .Construct(Arg.Any<int>());
            }
        }

        [Test]
        public void Should_be_able_to_construct_an_object_using_WithConstructor()
        {
            const int arg1 = 1;
            const float arg2 = 2f;


            {
                reflectionUtil.RequiresConstructorArgs(typeof(MyClassWithConstructor)).Returns(true);
                reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(arg1, arg2).Returns(new MyClassWithConstructor(arg1, arg2));
            }


            {
                myClassWithConstructorBuilder
                        .WithConstructor(() => new MyClassWithConstructor(arg1, arg2))
                        .Construct(1);
            }
        }

        [Test]
        public void WithConstructor_NotANewExpressionSupplied_Throws()
        {



            {
                var myClass = new MyClassWithConstructor(1, 2);
                Assert.Throws<ArgumentException>(() => myClassWithConstructorBuilder.WithConstructor(() => myClass));
            }
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithOptionalConstructorArgs()
        {
            // ctor: public MyClassWithOptionalConstructor(string arg1, int arg2)

            const string arg1 = "test";
            const int arg2 = 2;


            {
                reflectionUtil.CreateInstanceOf<MyClassWithOptionalConstructor>(arg1, arg2).Returns(new MyClassWithOptionalConstructor(arg1, arg2));
            }


            {
#pragma warning disable 0618 // (prevent warning for using obsolete method)
                myClassWithOptionalConstructorBuilder
                        .WithConstructor(() => new MyClassWithOptionalConstructor(arg1, arg2))
                        .Construct(1);
#pragma warning restore 0618
            }
        }

        [Test]
        public void ShouldBeAbleToUseWith()
        {


            {
                var myClass = new MyClass();

                builder.With(x => x.Float = 2f);
                builder.CallFunctions(myClass);

                Assert.That(myClass.Float, Is.EqualTo(2f));
            }
        }

        [Test]
        public void ShouldBeAbleToUseWith_WithAnIndex()
        {


            {
                var myClass = new MyClass();

                builder.With((x, idx) => x.StringOne = "String" + (idx + 5));
                builder.CallFunctions(myClass, 9);

                Assert.That(myClass.StringOne, Is.EqualTo("String14"));
            }
        }

        [Test]
        public void ShouldBeAbleToUseDo()
        {
            var myClass = Substitute.For<MyClass>();


            {
                myClass.DoSomething();
            }


            {
                builder.Do(x => x.DoSomething());
                builder.CallFunctions(myClass);
            }
        }

        [Test]
        public void ShouldBeAbleToUseANamingStrategy()
        {
            IPropertyNamer propertyNamer = Substitute.For<IPropertyNamer>();


            {
                propertyNamer.SetValuesOf(Arg.Any<MyClass>());
            }


            {
                builder.WithPropertyNamer(propertyNamer);
                builder.Name(new MyClass());
            }
        }

        [Test]
        public void ShouldBeAbleToUseBuild()
        {
            var myClass = new MyClass();
            IPropertyNamer propertyNamer = Substitute.For<IPropertyNamer>();


            {
                reflectionUtil.CreateInstanceOf<MyClass>().Returns(myClass);
                propertyNamer.SetValuesOf(Arg.Any<MyClass>());
            }


            {
                builder.WithPropertyNamer(propertyNamer);
                builder.With(x => x.Float = 2f);
                builder.Build();
            }
        }

        [Test]
        public void ShouldBeAbleToUseDoMultiple()
        {
            var builderSetup = new BuilderSettings();
            var myClass = Substitute.For<IMyInterface>();
            var list = new List<IMyOtherInterface> { Substitute.For<IMyOtherInterface>(), Substitute.For<IMyOtherInterface>(), Substitute.For<IMyOtherInterface>() };

            var builder2 = new ObjectBuilder<IMyInterface>(reflectionUtil, builderSetup);


            {
                myClass.Add(Arg.Any<IMyOtherInterface>());
            }


            {
                builder2.DoMultiple((x, y) => x.Add(y), list);
                builder2.CallFunctions(myClass);
            }
        }
    }
}