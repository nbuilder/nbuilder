using System;
using FizzWare.NBuilder.Extensions;
using FizzWare.NBuilder.Generators;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ExtensibileRandomValuePropertyNamerTests
    {
        private ExtensibleRandomValuePropertyNamer target;

        [SetUp]
        public void SetUp()
        {
            BuilderSetup.DisablePropertyNamingFor<MyClass, int>(x => x.ThisPropertyHasAGetterWhichThrowsAnException);
            target = new ExtensibleRandomValuePropertyNamer();
        }

        [Test]
        public void can_set_values_of()
        {
            var myClass = new MyClass();
            target.SetValuesOf(myClass);
            Assert.IsFalse(myClass.StringOne.IsDefaultValue());

            //NOTE: This test should pass some of the time with these lines un-commented. It will not always pass 
            //NOTE: because any non-nullable member could be the default value some of the time. However, in any
            //NOTE: member is the default all of the time, there is an error.
            //Assert.IsFalse(myClass.Char.IsDefaultValue());
            //Assert.IsFalse(myClass.Int.IsDefaultValue());
            //Assert.IsFalse(myClass.Long.IsDefaultValue());
            //Assert.IsFalse(myClass.Short.IsDefaultValue());
            //Assert.IsFalse(myClass.DateTime.IsDefaultValue());
            //Assert.IsFalse(myClass.Uint.IsDefaultValue());
            //Assert.IsFalse(myClass.Ulong.IsDefaultValue());
            //Assert.IsFalse(myClass.Ushort.IsDefaultValue());
            //Assert.IsFalse(myClass.Float.IsDefaultValue());
            //Assert.IsFalse(myClass.Byte.IsDefaultValue());
            //Assert.IsFalse(myClass.ByteEnumProperty.IsDefaultValue());
            //Assert.IsFalse(myClass.EnumProperty.IsDefaultValue());
            //Assert.IsFalse(myClass.PublicFieldInt.IsDefaultValue());
            //Assert.IsFalse(myClass.Decimal.IsDefaultValue());
            //Assert.IsFalse(myClass.Double.IsDefaultValue());
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
