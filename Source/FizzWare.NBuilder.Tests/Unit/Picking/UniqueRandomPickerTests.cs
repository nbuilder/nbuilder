using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class UniqueRandomPickerTests
    {
        [Test]
        public void UniqueRandomPickerShouldBeAbleToPickEntireList()
        {
            var testInput = new[] { 1, 2, 3, 4 };

            var results = Pick<int>.UniqueRandomList(testInput.Length).From(testInput);

            Assert.That(results.Count, Is.EqualTo(testInput.Length));
        }
    }
}