using Shouldly;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class BuilderTests
    {
        public class MyMasterClass
        {
            public string MasterStringProperty { get; set; }
            public MyClass SubClass { get; set; }

        }

        public class MyClass
        {
            public MyClass(){}
            public string StringProperty { get; set; }
        }

        [Fact]
        public void Default()
        {
            var builder = new Builder();

            var result = builder.CreateNew<MyClass>().Build();

            result.StringProperty.ShouldBe("StringProperty1");
        }

        [Fact]
        public void DisablePropertyNamingFor()
        {
            var settings = new BuilderSettings();
            settings.DisablePropertyNamingFor<MyClass, string>(m => m.StringProperty);
            var builder = new Builder(settings);

            var result = builder.CreateNew<MyClass>().Build();

            result.StringProperty.ShouldBe(null);
        }

        [Fact]
        public void BuilderShouldAllowSubPropertiesToBeSet()
        {
            var builder = new Builder();

            var result = builder.CreateNew<MyMasterClass>().BuildRecursive();

            result.MasterStringProperty.ShouldBe("MasterStringProperty1");
            result.SubClass.StringProperty.ShouldBe("StringProperty1");

        }
    }
}