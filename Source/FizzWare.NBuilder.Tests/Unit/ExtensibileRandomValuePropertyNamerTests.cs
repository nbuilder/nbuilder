using System;
using System.Reflection;
using FizzWare.NBuilder.Generators;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ExtensibileRandomValuePropertyNamerTests
    {
        private ExtensibleRandomValuePropertyNamer target;

        private MockRepository mocks;
        private IRandomGenerator randomGenerator;

        [SetUp]
        public void SetUp()
        {
            //BuilderSetup.DisablePropertyNamingFor<MyClass, int>(x => x.ThisPropertyHasAGetterWhichThrowsAnException);

            mocks = new MockRepository();

            randomGenerator = mocks.DynamicMock<IRandomGenerator>();
            target = new ExtensibleRandomValuePropertyNamer(randomGenerator);

            bool @bool = true;
            byte @byte = 1;
            char @char = 'a';
            var dateTime = new DateTime(2009, 01, 01);
            decimal @decimal = 1m;
            double @double = 1d;
            float @float = 1f;
            Guid guid = new Guid();
            int @int = 2;
            long @long = 3;
            string phrase = "some text";
            sbyte @sbyte = 4;
            short @short = 5;
            uint @uint = 6;
            ulong @ulong = 7;
            ushort @ushort = 8;
            MyEnum @enum = MyEnum.EnumValue3;

            using (mocks.Record())
            {
                randomGenerator.Expect(x => x.Boolean()).Return(@bool);
                randomGenerator.Expect(x => x.Byte()).Return(@byte);
                randomGenerator.Expect(x => x.Char()).Return(@char);
                randomGenerator.Expect(x => x.DateTime()).Return(dateTime);
                randomGenerator.Expect(x => x.Decimal()).Return(@decimal);
                randomGenerator.Expect(x => x.Double()).Return(@double);
                randomGenerator.Expect(x => x.Float()).Return(@float);
                randomGenerator.Expect(x => x.Guid()).Return(guid);
                randomGenerator.Expect(x => x.Int()).Return(@int);
                randomGenerator.Expect(x => x.Long()).Return(@long);
                randomGenerator.Expect(x => x.Phrase(50)).Return(@phrase);
                randomGenerator.Expect(x => x.SByte()).Return(@sbyte);
                randomGenerator.Expect(x => x.Short()).Return(@short);
                randomGenerator.Expect(x => x.UInt()).Return(@uint);
                randomGenerator.Expect(x => x.ULong()).Return(@ulong);
                randomGenerator.Expect(x => x.UShort()).Return(@ushort);
                randomGenerator.Expect(x => x.Enumeration(typeof(MyEnum))).Return(@enum);
            }
        }

        [Test]
        public void can_set_values_of()
        {
            using (mocks.Playback())
            {
                var myClass = new MyClass();
                target.SetValuesOf(myClass);
            }
        }

        [Test]
        public void replace_a_default_handler_names_with_new_handler()
        {
            var myClass = new MyClass();
            const int intValue = 234;
            target.NameWith(() => GetRandom.Int(intValue, intValue));
            target.SetValuesOf(myClass);
            Assert.That(myClass.Int, Is.EqualTo(intValue));
        }

        [Test]
        public void adding_handler_for_complex_type_names_using_the_added_handler()
        {
            var foo = new Foo();
            target.NameWith<Bar>(BarBuilder.New.Build);
            target.SetValuesOf(foo);
            Assert.AreEqual(BarBuilder.String1Length, foo.Bar.String1.Length);
            Assert.AreEqual(BarBuilder.String2Length, foo.Bar.String2.Length);
        }

        [Test]
        public void removing_a_handler_causes_members_of_that_type_to_not_be_named()
        {
            var myClass = new MyClass();
            target.DontName<string>();
            target.SetValuesOf(myClass);
            Assert.That(myClass.StringOne, Is.Null);
        }

        [Test]
        public void nullable_members_are_set_using_the_non_nullable_handler_when_no_nullable_handler_exists()
        {
            var foo = new Foo();
            target.SetValuesOf(foo);
            Assert.That(foo.DateTime, Is.Not.Null);
        }

        [Test]
        public void nullable_members_are_set_using_the_specifc_nullable_handler_and_not_the_non_nullable_one_for_te_same_type()
        {
            var foo = new Foo();
            target.NameWith<DateTime?>(() => GetRandom.DateTime(DateTime.Today, DateTime.Today));
            target.SetValuesOf(foo);
            Assert.That(foo.DateTime, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void when_naming_with_handler_that_take_memeberinfo_parameter__the_memeber_info_parameter_will_be_received()
        {
            var foo = new Foo();
            MemberInfo barMemberInfo = null;
            target.NameWith(m => 
            { 
                barMemberInfo = m;
                return new Bar();
            });

            MemberInfo dateTimeMemberInfo = null;
            target.NameWith(m =>
            {
                dateTimeMemberInfo = m;
                return DateTime.UtcNow;
            });
            
            target.SetValuesOf(foo);

            Assert.That(barMemberInfo.Name, Is.EqualTo("Bar"));
            Assert.That(barMemberInfo.DeclaringType, Is.EqualTo(typeof(Foo)));
            
            Assert.That(dateTimeMemberInfo.Name, Is.EqualTo("DateTime"));
            Assert.That(dateTimeMemberInfo.DeclaringType, Is.EqualTo(typeof(Foo)));
        }
    }

    public class BarBuilder
    {
        public static int String1Length = 9;
        public static int String2Length = 4;

        public static ISingleObjectBuilder<Bar> New
        {
            get
            {
                return Builder<Bar>.CreateNew()
                    .With(x => x.String1 = GetRandom.String(String1Length))
                    .With(x => x.String2 = GetRandom.String(String2Length));
            }
        }
    }

    public class Foo
    {
        public DateTime? DateTime { get; set; }
        public Bar Bar { get; set; }
    }

    public class Bar
    {
        public string String1 { get; set; }
        public string String2 { get; set; }
    }
}
