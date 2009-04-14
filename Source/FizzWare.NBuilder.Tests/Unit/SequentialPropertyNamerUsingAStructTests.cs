//using System.Collections.Generic;
//using FizzWare.NBuilder.Implementation;
//using FizzWare.NBuilder.PropertyNaming;
//using FizzWare.NBuilder.Tests.Unit.TestClasses;
//using NUnit.Framework;
//using NUnit.Framework.SyntaxHelpers;
//using Rhino.Mocks;

//namespace FizzWare.NBuilder.Tests.Unit
//{
//    [TestFixture]
//    public class SequentialPropertyNamerUsingAStructTests
//    {
//        private IList<MyStruct> theList;
//        private const int listSize = 1000;

//        [TestFixtureSetUp]
//        public void SetUp()
//        {
//            var reflectionUtil = MockRepository.GenerateStub<IReflectionUtil>();
//            reflectionUtil.Stub(x => x.IsDefaultValue(null)).IgnoreArguments().Return(true).Repeat.Any();

//            theList = new List<MyStruct>();

//            for (int i = 0; i < listSize; i++)
//                theList.Add(new MyStruct());

//            new SequentialPropertyNamer<MyStruct>(reflectionUtil).SetValuesOfAllIn(theList);
//        }

//        [Test]
//        public void ShouldNameStringPropertiesWithTheirNameAndSequenceNumber()
//        {
//            Assert.That(theList[0].String, Is.EqualTo("String" + 1));
//            Assert.That(theList[9].String, Is.EqualTo("String" + 10));
//        }

//        [Test]
//        public void ShouldNameIntProperties()
//        {
//            Assert.That(theList[0].Int, Is.EqualTo(1));
//            Assert.That(theList[9].Int, Is.EqualTo(10));
//        }
//    }
//}