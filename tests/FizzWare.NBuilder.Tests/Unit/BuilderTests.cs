using Shouldly;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class BuilderTests
    {

        public class MyClass
        {
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
    }
}