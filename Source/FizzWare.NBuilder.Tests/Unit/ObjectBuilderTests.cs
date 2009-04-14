using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    public interface IMyClass
    {
        void Add(IMyOtherClass myOtherClass);
    }

    public interface IMyOtherClass
    {
        
    }

    [TestFixture]
    public class ObjectBuilderTests
    {
        private IReflectionUtil reflectionUtil;
        private ObjectBuilder<MyClass> builder;
        private ObjectBuilder<MyClassWithConstructor> myClassWithConstructorBuilder;
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            reflectionUtil = mocks.DynamicMock<IReflectionUtil>();

            builder = new ObjectBuilder<MyClass>(reflectionUtil);
            myClassWithConstructorBuilder = new ObjectBuilder<MyClassWithConstructor>(reflectionUtil);
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
        [ExpectedException(typeof(TypeCreationException))]
        public void ShouldThrowIfNoConstructorArgsPassed()
        {
            using (mocks.Record())
                reflectionUtil.Expect(x => x.RequiresConstructorArgs(typeof(MyClassWithConstructor))).Return(true);

            using (mocks.Playback())
                myClassWithConstructorBuilder.Construct();
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
            IPropertyNamer<MyClass> propertyNamer = MockRepository.GenerateMock<IPropertyNamer<MyClass>>();

            using (mocks.Record())
            {
                propertyNamer.Expect(x => x.SetValuesOf(Arg<MyClass>.Is.TypeOf));    
            }

            using (mocks.Playback())
            {
                builder.WithNamingStrategy(propertyNamer);
                builder.Name(new MyClass());
            }
        }

        [Test]
        public void ShouldBeAbleToUseBuild()
        {
            var myClass = new MyClass();
            IPropertyNamer<MyClass> propertyNamer = MockRepository.GenerateMock<IPropertyNamer<MyClass>>();

            using (mocks.Record())
            {
                reflectionUtil.Expect(x => x.CreateInstanceOf<MyClass>()).Return(myClass);
                propertyNamer.Expect(x => x.SetValuesOf(Arg<MyClass>.Is.TypeOf));
            }

            using (mocks.Playback())
            {
                builder.WithNamingStrategy(propertyNamer);
                builder.With(x => x.Float = 2f);
                builder.Build();
            }
        }

        [Test]
        public void ShouldBeAbleToUseDoMultiple()
        {
            var myClass = mocks.DynamicMock<IMyClass>();
            var list = new List<IMyOtherClass> { mocks.Stub<IMyOtherClass>(), mocks.Stub<IMyOtherClass>(), mocks.Stub<IMyOtherClass>() };

            var builder2 = new ObjectBuilder<IMyClass>(reflectionUtil);

            using (mocks.Record())
            {
                const int listSize = 3;
                myClass.Expect(x => x.Add(Arg<IMyOtherClass>.Is.TypeOf)).Repeat.Times(listSize);
            }

            using (mocks.Playback())
            {
                builder2.DoMultiple((x, y) => x.Add(y), list);
                builder2.CallFunctions(myClass);
            }
        }
    }
}