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
        BuilderSetup builderSetup ;
        private ExtensibleRandomValuePropertyNamer target;

        private MockRepository mocks;
        private IRandomGenerator randomGenerator;

        [SetUp]
        public void SetUp()
        {
            builderSetup = new BuilderSetup();
            mocks = new MockRepository();

            randomGenerator = mocks.DynamicMock<IRandomGenerator>();
            target = new ExtensibleRandomValuePropertyNamer(randomGenerator, builderSetup);

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
            // Arrange
            var myClass = new MyClass();
            target.NameWith<SimpleClass>(SimpleClassBuilder.New.Build);

            // Act
            target.SetValuesOf(myClass);

            // Assert
            Assert.AreEqual(SimpleClassBuilder.String1Length, myClass.SimpleClassProperty.String1.Length);
            Assert.AreEqual(SimpleClassBuilder.String2Length, myClass.SimpleClassProperty.String2.Length);
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
            var myClass = new MyClass();
            target.SetValuesOf(myClass);
            Assert.That(myClass.NullableInt, Is.Not.Null);
        }

        [Test]
        public void nullable_members_are_set_using_the_specifc_nullable_handler_and_not_the_non_nullable_one_for_te_same_type()
        {
            var myClass = new MyClass();
            target.NameWith<int?>(() => 500);
            target.SetValuesOf(myClass);
            Assert.That(myClass.NullableInt, Is.EqualTo(500));
        }

        [Test]
        public void when_naming_with_handler_that_takes_memeberinfo_parameter__the_member_info_parameter_will_be_received()
        {
            var foo = new MyClass();
            MemberInfo simpleClassMemberInfo = null;
            target.NameWith(m => 
            { 
                simpleClassMemberInfo = m;
                return new SimpleClass();
            });

            MemberInfo dateTimeMemberInfo = null;
            target.NameWith(m =>
            {
                dateTimeMemberInfo = m;
                return DateTime.UtcNow;
            });
            
            target.SetValuesOf(foo);

            Assert.That(simpleClassMemberInfo.Name, Is.EqualTo("SimpleClassProperty"));
            Assert.That(simpleClassMemberInfo.DeclaringType, Is.EqualTo(typeof(MyClass)));
            
            Assert.That(dateTimeMemberInfo.Name, Is.EqualTo("DateTime"));
            Assert.That(dateTimeMemberInfo.DeclaringType, Is.EqualTo(typeof(MyClass)));
        }

        [Test]
        public void SetValuesOf_IgnoredProperty_HonorsIgnore()
        {
            // Arrange
            var myClass = new MyClass();
            builderSetup.DisablePropertyNamingFor<MyClass, long>(x => x.Long);

            target.NameWith<long>(() => 50);

            // Act
            target.SetValuesOf(myClass);

            // Assert
            Assert.That(myClass.Long, Is.EqualTo(default(long)));
        }

        
    }
}
