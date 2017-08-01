using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class ListBuilderTests_WithAClassThatHasAParameterlessConstructor
    {
        [Theory]
        [InlineData(10, 5)]
        [InlineData(10, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 1)]
        public void ShouldBeAbleToUseRandom(int listSize, int randomItems)
        {
            var objects = Builder<MyClass>
                .CreateListOfSize(listSize)
                .Random(randomItems)
                .With(x => x.StringOne = "TestRandom")
                .Build();

            var numObjectsWithRandomValue = objects.Count(x => x.StringOne.Equals("TestRandom"));

            randomItems.ShouldBe(numObjectsWithRandomValue);
        }

        [Fact]
        public void PropertiesShouldBeSetSequentially()
        {
            var list = new Builder().CreateListOfSize<MyClass>(10).Build();

            list[0].StringOne.ShouldBe("StringOne1");
            list[9].StringOne.ShouldBe("StringOne10");
            list[0].EnumProperty.ShouldBe(MyEnum.EnumValue1);
            list[9].EnumProperty.ShouldBe(MyEnum.EnumValue5);
        }

        [Fact]
        public void Random_test()
        {
            var items = new Builder().CreateListOfSize<MyClass>(40)
                .Random(10)
                .With(x => x.EnumProperty = MyEnum.EnumValue1)
                .Random(10)
                .With(x => x.EnumProperty = MyEnum.EnumValue2)
                .Random(10)
                .With(x => x.EnumProperty = MyEnum.EnumValue3)
                .Random(10)
                .With(x => x.EnumProperty = MyEnum.EnumValue4)
                .Build();

            items.Count().ShouldBe(40);
            items.Count(x => x.EnumProperty == MyEnum.EnumValue1).ShouldBe(10);
            items.Count(x => x.EnumProperty == MyEnum.EnumValue2).ShouldBe(10);
            items.Count(x => x.EnumProperty == MyEnum.EnumValue3).ShouldBe(10);
            items.Count(x => x.EnumProperty == MyEnum.EnumValue4).ShouldBe(10);
        }

        [Fact]
        public void should_be_able_to_disable_property_naming_for_an_inherited_property()
        {
            var builderSettings = new BuilderSettings();
            try
            {
                builderSettings.DisablePropertyNamingFor<MyConcreteClass, int>(x => x.PropertyInAbstractClass);

                var list = new Builder(builderSettings).CreateListOfSize<MyConcreteClass>(10).Build();

                list[0].PropertyInAbstractClass.ShouldBe(0);
                list[0].PropertyInInheritedClass.ShouldBe(1);

                list[9].PropertyInAbstractClass.ShouldBe(0);
                list[9].PropertyInInheritedClass.ShouldBe(10);
            }
            finally
            {
                builderSettings.ResetToDefaults();
            }
        }

        [Fact]
        public void ShouldBeAbleToCreateAList()
        {
            var list = Builder<MyClass>.CreateListOfSize(10).Build();

            list.Count.ShouldBe(10);
        }

        [Fact]
        public void ShouldBeAbleToDisableAutomaticPropertyNaming()
        {
            var builderSettings = new BuilderSettings();
            try
            {
                builderSettings.AutoNameProperties = false;
                var list = new Builder(builderSettings).CreateListOfSize<MyClass>(10).Build();

                list[0].Int.ShouldBe(0);
                list[9].Int.ShouldBe(0);
            }
            finally
            {
                builderSettings.ResetToDefaults();
            }
        }

        [Fact]
        public void ShouldBeAbleToDisableAutomaticPropertyNamingForASpecificFieldOfASpecificType()
        {
            var builderSettings = new BuilderSettings();
            try
            {
                builderSettings.DisablePropertyNamingFor<MyClass, int>(x => x.Int);

                var list = new Builder(builderSettings).CreateListOfSize<MyClass>(10).Build();

                list[0].Int.ShouldBe(0);
                list[0].Long.ShouldBe(1);

                list[9].Int.ShouldBe(0);
                list[9].Long.ShouldBe(10);
            }
            finally
            {
                builderSettings.ResetToDefaults();
            }
        }

        [Fact]
        public void ShouldBeAbleToDisableAutoPropertyNaming()
        {
            var builderSettings = new BuilderSettings();
            try
            {
                builderSettings.AutoNameProperties = false;

                var list = new Builder(builderSettings).CreateListOfSize<MyClass>(10).Build();

                list[0].Int.ShouldBe(0);
                list[9].Int.ShouldBe(0);

                list[0].StringOne.ShouldBeNull();
                list[9].StringOne.ShouldBeNull();
            }
            finally
            {
                builderSettings.AutoNameProperties = true;
            }
        }

        [Fact]
        public void ShouldBeAbleToSpecifyACustomPropertyNamerForASpecificType()
        {
            var builderSettings = new BuilderSettings();
            var propertyNamer = Substitute.For<IPropertyNamer>();

            builderSettings.SetPropertyNamerFor<MyClass>(propertyNamer);

            new Builder(builderSettings).CreateListOfSize<MyClass>(10).Build();
            new Builder(builderSettings).CreateListOfSize<SimpleClass>(10).Build();

            propertyNamer.Received().SetValuesOfAllIn(Arg.Any<IList<MyClass>>());
        }

        [Fact]
        public void ShouldBeAbleToSpecifyADefaultCustomPropertyNamer()
        {
            var builderSettings = new BuilderSettings();
            try
            {
                builderSettings.SetDefaultPropertyNamer(new MockPropertyNamerTests());
                new Builder(builderSettings).CreateListOfSize<MyClass>(10).Build();
                MockPropertyNamerTests.SetValuesOfAllInCallCount.ShouldBe(1);
            }
            finally
            {
                builderSettings.ResetToDefaults();
            }
        }

        [Fact]
        public void ShouldBeAbleToUseAndTheNext()
        {
            var builderSettings = new BuilderSettings();
            var titleone = "TitleOne";
            var titletwo = "TitleTwo";

            var productList =
                Builder<MyClass>
                    .CreateListOfSize(4)
                    .TheFirst(2)
                    .With(x => x.StringOne = titleone)
                    .TheNext(2)
                    .With(x => x.StringOne = titletwo)
                    .Build();

            productList[0].StringOne.ShouldBe(titleone);
            productList[1].StringOne.ShouldBe(titleone);
            productList[2].StringOne.ShouldBe(titletwo);
            productList[3].StringOne.ShouldBe(titletwo);
        }

        [Fact]
        public void ShouldBeAbleToUseAndTheNextAfterASectionDeclaration()
        {
            var builderSettings = new BuilderSettings();
            var objects = Builder<MyClass>
                .CreateListOfSize(10)
                .Section(0, 4)
                .With(x => x.Int = 1)
                .TheNext(3)
                .With(x => x.Int = 2)
                .Build();

            objects[0].Int.ShouldBe(1);
            objects[1].Int.ShouldBe(1);
            objects[2].Int.ShouldBe(1);
            objects[3].Int.ShouldBe(1);
            objects[4].Int.ShouldBe(1);
            objects[5].Int.ShouldBe(2);
            objects[6].Int.ShouldBe(2);
            objects[7].Int.ShouldBe(2);
        }

        [Fact]
        public void ShouldBeAbleToUseAndThePrevious()
        {
            var builderSettings = new BuilderSettings();
            var titleone = "TitleOne";
            var titletwo = "TitleTwo";

            var productList =
                Builder<MyClass>
                    .CreateListOfSize(4)
                    .TheLast(2)
                    .With(x => x.StringOne = titletwo)
                    .ThePrevious(2)
                    .With(x => x.StringOne = titleone)
                    .Build();

            productList[0].StringOne.ShouldBe(titleone);
            productList[1].StringOne.ShouldBe(titleone);
            productList[2].StringOne.ShouldBe(titletwo);
            productList[3].StringOne.ShouldBe(titletwo);
        }

        [Fact]
        public void ShouldBeAbleToUseDo()
        {
            var builderSettings = new BuilderSettings();
            var myOtherClass = Builder<SimpleClass>.CreateNew().Build();

            var objects = Builder<MyClass>.CreateListOfSize(5).All().Do(x => x.Add(myOtherClass)).Build();

            for (var i = 0; i < objects.Count; i++)
            {
                objects[i].SimpleClasses.Count.ShouldBe(1);
            }
        }

        [Fact]
        public void ShouldBeAbleToUseMultipleTheFirsts()
        {
            var title = "FirstTitle";
            var overwrittenTitle = "OverwrittenTitle";

            var list =
                Builder<MyClass>.CreateListOfSize(10)
                    .TheFirst(5)
                    .Do(x =>x.StringOne = title)
                    .TheFirst(5)
                    .Do(x => x.StringOne = overwrittenTitle)
                    .Build();

            list.Count.ShouldBe(10);
            list[0].StringOne.ShouldBe(overwrittenTitle);
            list[4].StringOne.ShouldBe(overwrittenTitle);
        }

        [Fact]
        public void ShouldBeAbleToUseTheFirst()
        {
            var specialTitle = "SpecialTitle";

            var list =
                Builder<MyClass>.CreateListOfSize(10).TheFirst(5).With(x => x.StringOne = specialTitle).Build();

            // I want the asserts here to serve as documentation
            // so it's obvious how it works for anyone glancing at this test
            list[0].StringOne.ShouldBe(specialTitle);
            list[1].StringOne.ShouldBe(specialTitle);
            list[2].StringOne.ShouldBe(specialTitle);
            list[3].StringOne.ShouldBe(specialTitle);
            list[4].StringOne.ShouldBe(specialTitle);
            list[5].StringOne.ShouldBe("StringOne6");
            list[6].StringOne.ShouldBe("StringOne7");
            list[7].StringOne.ShouldBe("StringOne8");
            list[8].StringOne.ShouldBe("StringOne9");
            list[9].StringOne.ShouldBe("StringOne10");
        }

        [Fact]
        public void ShouldBeAbleToUseTheLast()
        {
            var specialTitle = "SpecialTitle";

            var list =
                Builder<MyClass>.CreateListOfSize(10).TheLast(5).With(x => x.StringOne = specialTitle).Build();

            list[0].StringOne.ShouldBe("StringOne1");
            list[1].StringOne.ShouldBe("StringOne2");
            list[2].StringOne.ShouldBe("StringOne3");
            list[3].StringOne.ShouldBe("StringOne4");
            list[4].StringOne.ShouldBe("StringOne5");
            list[5].StringOne.ShouldBe(specialTitle);
            list[6].StringOne.ShouldBe(specialTitle);
            list[7].StringOne.ShouldBe(specialTitle);
            list[8].StringOne.ShouldBe(specialTitle);
            list[9].StringOne.ShouldBe(specialTitle);
        }
    }
}