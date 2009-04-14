//using FizzWare.NBuilder.Tests.Unit.TestClasses;
//using NUnit.Framework;
//using NUnit.Framework.SyntaxHelpers;

//namespace FizzWare.NBuilder.Tests.Integration
//{
//    [TestFixture]
//    public class UsingTheSingleObjectBuilderWithAStruct
//    {
//        [Test]
//        public void ShouldBeAbleToCreateAnObject()
//        {
//            var obj = Builder<MyStruct>.CreateNew().Build();

//            Assert.That(obj.Int, Is.EqualTo(1));
//            Assert.That(obj.String, Is.EqualTo("String1"));
//        }
//    }
//}