using FizzWare.NBuilder;
using NBuilderWebsite.Controllers;
using NBuilderWebsite.Models;
using NUnit.Framework;

namespace NBuilder.Website.Tests
{
    public class Contact_form_tests
    {
        private ContactController target;

        [TestFixtureSetUp]
        public void Before_all_tests()
        {
            DatabaseHelper.ExecuteFile("CreateDatabaseTables.sql");
        }

        [SetUp]
        public void Before_each_test()
        {
            target = new ContactController();
        }

        [TestFixture]
        public class When_submitting_a_contact_request : Contact_form_tests
        {
            [Test]
            public void It_saves_the_contact_request_in_the_db()
            {
                // Arrange
                var entry = Builder<ContactEntry>.CreateNew().Build();
                
                // Act
                target.Index(entry);

                // Assert
                var contents = DatabaseHelper.GetContentsOf("ContactEntry");
                Assert.That(contents.Rows[0]["Name"], Is.EqualTo(entry.Name));
                Assert.That(contents.Rows[0]["EmailAddress"], Is.EqualTo(entry.EmailAddress));
                Assert.That(contents.Rows[0]["Message"], Is.EqualTo(entry.Message));
            }
        }
    }
}