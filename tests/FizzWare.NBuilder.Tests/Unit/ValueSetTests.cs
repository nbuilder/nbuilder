using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class ValueSetTests
    {
        public ValueSetTests()
        {

        }

        [Fact]
        public void Next_FirstItem()
        {
            var set = new ValueSet<string>("fredbob", "foobar", "pat boone", "debbie boone");

            set.Next().ShouldBe("fredbob");
            set.Next().ShouldBe("foobar");
        }

        [Fact]
        public void Next_AllItems()
        {
            var set = new ValueSet<string>("fredbob", "foobar", "pat boone", "debbie boone");

            set.Next().ShouldBe("fredbob");
            set.Next().ShouldBe("foobar");
            set.Next().ShouldBe("pat boone");
            set.Next().ShouldBe("debbie boone");
        }

        [Fact]
        public void Next_WrapAround()
        {
            var set = new ValueSet<string>("fredbob", "foobar", "pat boone", "debbie boone");

            set.Next().ShouldBe("fredbob");
            set.Next().ShouldBe("foobar");
            set.Next().ShouldBe("pat boone");
            set.Next().ShouldBe("debbie boone");
            set.Next().ShouldBe("fredbob");
        }

        [Fact]
        public void Random_ValuesAreGeneratedSomewhatRandomly()
        {
            var values = new string[] { "fredbob", "foobar", "pat boone", "debbie boone" };

            var results = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                // The idea here is to create a new set every time with
                // enough delay so that seeding the random number generator
                // yields a different random sequence. By creatinga new set
                // every time, collecting the results, and asserting we have
                // a complete list of values, we confirm that we are indeed
                // getting all of the values even in the first call to Random()
                // on the set. This proves that we're not just reimplementing
                // Next()
                //
                System.Threading.Thread.Sleep(10);
                var set = new ValueSet<string>(values);
                results.Add(set.Random());
            }

            var unique = results.Distinct().OrderBy(value => value).ToHashSet<string>();
            var compare = values.OrderBy(value => value).ToHashSet<string>();

            unique.ShouldBe(compare);
        }

        [Fact]
        public void Random_AllValuesAreRepresentedInASufficientlyLargeDistribution()
        {
            var set = new ValueSet<string>("fredbob", "foobar", "pat boone", "debbie boone");
            
            var results = new List<string>();
            for (var i = 0; i< 100; i++)
            {
                results.Add(set.Random());
            }

            var unique = results.Distinct().OrderBy(value => value).ToHashSet<string>();
            var compare = set.OrderBy(value => value).ToHashSet<string>();
            
            unique.ShouldBe(compare);
       }

        public class Demo
        {
            public string Name { get; set; }
        }

        [Fact]
        public void Sample_UsingInABuilderContext()
        {
            var names = new ValueSet<string>("fredbob", "foobar", "pat boone", "debbie boone");

            var demos = new Builder()
                .CreateListOfSize<Demo>(5)
                .All()
                .With(row => row.Name = names.Next())
                .Build()
                ;

            demos[0].Name.ShouldBe("fredbob");
            demos[1].Name.ShouldBe("foobar");
            demos[4].Name.ShouldBe("fredbob");

        }

    }
}
