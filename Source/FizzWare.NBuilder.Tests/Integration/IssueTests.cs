using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;
using NSubstitute;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class IssueTests
    {

        //http://code.google.com/p/nbuilder/issues/detail?id=68
        [Test]
        public void Issue68_ReadonlyProperty_ShouldNotWriteTraceDueToAttemptingToSetAPropertyThatCannotBeSet()
        {
            var builderSetup = new BuilderSettings();
            var traceListener = Substitute.For<TraceListener>();
            Trace.Listeners.Add(traceListener);

            var product = new Builder(builderSetup)
                           .CreateListOfSize< DataModel>(2)
                           .All()
                           .With(x => x.ExpirationMonth = "01")
                           .With(x => x.ExpirationYear = "2010")
                           .Build();

            traceListener.DidNotReceiveWithAnyArgs().WriteLine("");
            Trace.Listeners.Remove(traceListener);
        }

        public class DataModel
        {
            public string ExpirationMonth { get; set; }
            public string ExpirationYear { get; set; }

            public string ExpirationDate
            {
                get
                {
                    return String.Format("{0}/{1}/{2}", int.Parse(ExpirationYear), int.Parse(ExpirationMonth), 1);
                }
            }
        }
    }
}
