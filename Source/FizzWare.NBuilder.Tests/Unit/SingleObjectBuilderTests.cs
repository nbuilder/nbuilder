//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using FizzWare.NBuilder.Implementation;
//using FizzWare.NBuilder.PropertyValueNaming;
//using NUnit.Framework;
//using FizzWare.NBuilder.Tests.Unit.TestClasses;
//using Rhino.Mocks;
//using NUnit.Framework.SyntaxHelpers;

//namespace FizzWare.NBuilder.Tests.Unit
//{
//    [TestFixture]
//    public class SingleObjectBuilderTests
//    {
//        private IReflectionUtil reflectionUtil;
//        private IPropertyNamer<MyClass> namingStrategy;
//        private readonly MyClass myClass = new MyClass();
//        private const int listSize = 10;

//        private MockRepository mocks;

//        [SetUp]
//        public void SetUp()
//        {
//            mocks = new MockRepository();

//            reflectionUtil = mocks.DynamicMock<IReflectionUtil>();
//            namingStrategy = mocks.DynamicMock<IPropertyNamer<MyClass>>();

//            using (mocks.Record())
//            {
//                reflectionUtil.ex
//            }
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            mocks.VerifyAll();
//        }

//        [Test]
//        public void ShouldBeAbleToCreateAnObject()
//        {
//            using (mocks.Playback())
//            {
//                var obj = Builder<MyClass>
//                    .CreateNew()
//                    .With(x => x.Float = 1f)
//                    .With(x => x.Double = 1d)
//                    .Do(x => x.DoSomething())
//                    .Build();

//                Assert.That(obj, Is.Not.Null);
//            }
//        }
//    }
//}
