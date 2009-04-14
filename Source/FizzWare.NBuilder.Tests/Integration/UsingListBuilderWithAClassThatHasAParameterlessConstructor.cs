using System;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Tests.TestClasses;
using FizzWare.NBuilder.Tests.Unit;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Linq.Expressions;
using System.Reflection;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class UsingListBuilderWithAClassThatHasAParameterlessConstructor
    {
        [Test]
        public void ShouldBeAbleToCreateAList()
        {
            var list =
                Builder<MyClass>.CreateListOfSize(10).Build();

            Assert.That(list, Has.Count(10));
        }

        [Test]
        public void PropertiesShouldBeSetSequentially()
        {
            var list = Builder<MyClass>.CreateListOfSize(10).Build();

            Assert.That(list[0].StringOne, Is.EqualTo("StringOne1"));
            Assert.That(list[9].StringOne, Is.EqualTo("StringOne10"));
        }

        [Test]
        public void ShouldBeAbleToUseWhereTheFirst()
        {
            var specialTitle = "SpecialTitle";

            var list =
                Builder<MyClass>.CreateListOfSize(10).WhereTheFirst(5).Have(x => x.StringOne = specialTitle).Build();

            // I want the asserts here to serve as documentation
            // so it's obvious how it works for anyone glancing at this test
            Assert.That(list[0].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[1].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[2].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[3].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[4].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[5].StringOne, Is.EqualTo("StringOne6"));
            Assert.That(list[6].StringOne, Is.EqualTo("StringOne7"));
            Assert.That(list[7].StringOne, Is.EqualTo("StringOne8"));
            Assert.That(list[8].StringOne, Is.EqualTo("StringOne9"));
            Assert.That(list[9].StringOne, Is.EqualTo("StringOne10"));
        }

        [Test]
        public void ShouldBeAbleToUseWhereTheLast()
        {
            var specialTitle = "SpecialTitle";

            var list =
                Builder<MyClass>.CreateListOfSize(10).WhereTheLast(5).Have(x => x.StringOne = specialTitle).Build();

            Assert.That(list[0].StringOne, Is.EqualTo("StringOne1"));
            Assert.That(list[1].StringOne, Is.EqualTo("StringOne2"));
            Assert.That(list[2].StringOne, Is.EqualTo("StringOne3"));
            Assert.That(list[3].StringOne, Is.EqualTo("StringOne4"));
            Assert.That(list[4].StringOne, Is.EqualTo("StringOne5"));
            Assert.That(list[5].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[6].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[7].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[8].StringOne, Is.EqualTo(specialTitle));
            Assert.That(list[9].StringOne, Is.EqualTo(specialTitle));   
        }

        [Test]
        public void ShouldBeAbleToUseMultipleWhereTheFirsts()
        {
            var title = "FirstTitle";
            var overwrittenTitle = "OverwrittenTitle";

            var list =
                Builder<MyClass>.CreateListOfSize(10)
                                .WhereTheFirst(5)
                                    .Have(x => x.StringOne = title)
                                .WhereTheFirst(5)
                                    .Have(x => x.StringOne = overwrittenTitle)
                                .Build();

            Assert.That(list, Has.Count(10));
            Assert.That(list[0].StringOne, Is.EqualTo(overwrittenTitle));
            Assert.That(list[4].StringOne, Is.EqualTo(overwrittenTitle));
        }

        [Test]
        public void ShouldBeAbleToUseAndTheNext()
        {
            var titleone = "TitleOne";
            var titletwo = "TitleTwo";

            var productList =
                Builder<MyClass>
                .CreateListOfSize(4)
                    .WhereTheFirst(2)
                        .Have(x => x.StringOne = titleone)
                    .AndTheNext(2)
                        .Have(x => x.StringOne = titletwo)
                    .Build();

            Assert.That(productList[0].StringOne, Is.EqualTo(titleone));
            Assert.That(productList[1].StringOne, Is.EqualTo(titleone));
            Assert.That(productList[2].StringOne, Is.EqualTo(titletwo));
            Assert.That(productList[3].StringOne, Is.EqualTo(titletwo));
        }

        [Test]
        public void ShouldBeAbleToUseAndThePrevious()
        {
            var titleone = "TitleOne";
            var titletwo = "TitleTwo";

            var productList =
                Builder<MyClass>
                .CreateListOfSize(4)
                    .WhereTheLast(2)
                        .Have(x => x.StringOne = titletwo)
                    .AndThePrevious(2)
                        .Have(x => x.StringOne = titleone)
                    .Build();

            Assert.That(productList[0].StringOne, Is.EqualTo(titleone));
            Assert.That(productList[1].StringOne, Is.EqualTo(titleone));
            Assert.That(productList[2].StringOne, Is.EqualTo(titletwo));
            Assert.That(productList[3].StringOne, Is.EqualTo(titletwo));
        }

        [Test]
        public void ShouldBeAbleToUseHaveDoneToThem()
        {
            var myOtherClass = Builder<SimpleClass>.CreateNew().Build();

            var objects = Builder<MyClass>.CreateListOfSize(5).WhereAll().HaveDoneToThem(x => x.Add(myOtherClass)).Build();

            for (int i = 0; i < objects.Count; i++)
            {
                Assert.That(objects[i].SimpleClasses.Count, Is.EqualTo(1));
            }
        }
    }
}