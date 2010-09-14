using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ObjectBuilderTests
    {
        private IReflectionUtil reflectionUtil;
        private ObjectBuilder<MyClass> builder;
        private ObjectBuilder<MyClassWithConstructor> myClassWithConstructorBuilder;
        private ObjectBuilder<MyClassWithOptionalConstructor> myClassWithOptionalConstructorBuilder;
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            reflectionUtil = mocks.DynamicMock<IReflectionUtil>();

            builder = new ObjectBuilder<MyClass>(reflectionUtil);
            myClassWithConstructorBuilder = new ObjectBuilder<MyClassWithConstructor>(reflectionUtil);
            myClassWithOptionalConstructorBuilder = new ObjectBuilder<MyClassWithOptionalConstructor>(reflectionUtil);
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithNoConstructorArgs()
        {
            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.RequiresConstructorArgs(typeof(MyClass))).Return(false);
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClass>()).Return(new MyClass());
            }

            using (mocks.Playback())
                builder.Construct();
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithConstructorArgs()
        {
            const int arg1 = 1;
            const float arg2 = 2f;

            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.RequiresConstructorArgs(typeof(MyClassWithConstructor))).Return(true);
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClassWithConstructor>(arg1, arg2)).Return(new MyClassWithConstructor(arg1, arg2));
            }

            using (mocks.Playback())
            {
                myClassWithConstructorBuilder
                        .WithConstructorArgs(arg1, arg2)
                        .Construct();
            }
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithNullableConstructorArgs_using_expression_syntax()
        {
            const string arg1 = null;

            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.RequiresConstructorArgs(typeof(MyClassWithConstructor))).Return(true);
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClassWithConstructor>(arg1)).Return(new MyClassWithConstructor(arg1));
            }

            using (mocks.Playback())
            {
                myClassWithConstructorBuilder
                        .WithConstructor( () => new MyClassWithConstructor(arg1) )
                        .Construct();
            }
        }

        [Test]
        public void Should_be_able_to_construct_an_object_using_WithConstructor()
        {
            const int arg1 = 1;
            const float arg2 = 2f;

            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.RequiresConstructorArgs(typeof(MyClassWithConstructor))).Return(true);
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClassWithConstructor>(arg1, arg2)).Return(new MyClassWithConstructor(arg1, arg2));
            }

            using (mocks.Playback())
            {
                myClassWithConstructorBuilder
                        .WithConstructor(() => new MyClassWithConstructor(arg1, arg2))
                        .Construct();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WithConstructor_should_complain_if_the_expression_is_not_a_NewExpression()
        {
            using (mocks.Record())
            {}

            using (mocks.Playback())
            {
                var myClass = new MyClassWithConstructor(1, 2);

                myClassWithConstructorBuilder
                        .WithConstructor( () => myClass)
                        .Construct();
            }
        }

        [Test]
        public void ShouldBeAbleToConstructAnObjectWithOptionalConstructorArgs()
        {
            // ctor: public MyClassWithOptionalConstructor(string arg1, int arg2)

            const string arg1 = "test";
            const int arg2 = 2;

            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClassWithOptionalConstructor>(arg1, arg2)).Return(new MyClassWithOptionalConstructor(arg1, arg2));
            }

            using (mocks.Playback())
            {
                myClassWithOptionalConstructorBuilder
                        .WithConstructorArgs(arg1, arg2)
                        .Construct();
            }
        }

        [Test]
        public void ShouldBeAbleToUseWith()
        {
            using (mocks.Record())
            {
                
            }

            using (mocks.Playback())
            {
                var myClass = new MyClass();

                builder.With(x => x.Float = 2f);
                builder.CallFunctions(myClass);

                Assert.That(myClass.Float, Is.EqualTo(2f));
            }
        }

        [Test]
        public void ShouldBeAbleToUseDo()
        {
            var myClass = MockRepository.GenerateMock<MyClass>();

            using (mocks.Record())
            {
                myClass.Expect(x => x.DoSomething());
            }

            using (mocks.Playback())
            {
                builder.Do(x => x.DoSomething());
                builder.CallFunctions(myClass);
            }
        }

        [Test]
        public void ShouldBeAbleToUseANamingStrategy()
        {
            IPropertyNamer propertyNamer = MockRepository.GenerateMock<IPropertyNamer>();

            using (mocks.Record())
            {
                propertyNamer.Expect(x => x.SetValuesOf(Arg<MyClass>.Is.TypeOf));    
            }

            using (mocks.Playback())
            {
                builder.WithPropertyNamer(propertyNamer);
                builder.Name(new MyClass());
            }
        }

        [Test]
        public void ShouldBeAbleToUseBuild()
        {
            var myClass = new MyClass();
            IPropertyNamer propertyNamer = MockRepository.GenerateMock<IPropertyNamer>();

            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClass>()).Return(myClass);
                propertyNamer.Expect(x => x.SetValuesOf(Arg<MyClass>.Is.TypeOf));
            }

            using (mocks.Playback())
            {
                builder.WithPropertyNamer(propertyNamer);
                builder.With(x => x.Float = 2f);
                builder.Build();
            }
        }

        [Test]
        public void ShouldBeAbleToUseDoMultiple()
        {
            var myClass = mocks.DynamicMock<IMyInterface>();
            var list = new List<IMyOtherInterface> { mocks.Stub<IMyOtherInterface>(), mocks.Stub<IMyOtherInterface>(), mocks.Stub<IMyOtherInterface>() };

            var builder2 = new ObjectBuilder<IMyInterface>(reflectionUtil);

            using (mocks.Record())
            {
                const int listSize = 3;
                myClass.Expect(x => x.Add(Arg<IMyOtherInterface>.Is.TypeOf)).Repeat.Times(listSize);
            }

            using (mocks.Playback())
            {
                builder2.DoMultiple((x, y) => x.Add(y), list);
                builder2.CallFunctions(myClass);
            }
        }
    }
}