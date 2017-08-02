using System.Collections.Generic;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Tests.Integration
{
    public class MockPropertyNamerTests : IPropertyNamer
    {
        public static int SetValuesOfAllInCallCount;
        public static int SetValuesOf_obj_CallCount;
        public static int SetValuesOf_obj_sequenceNumber_sequenceIdentifier_CallCount;

        public void SetValuesOfAllIn<T>(IList<T> obj)
        {
            SetValuesOfAllInCallCount++;
        }

        public void SetValuesOf<T>(T obj)
        {
            SetValuesOf_obj_CallCount++;
        }

        public void SetValuesOf<T>(T obj, int sequenceNumber, string sequenceIdentifier)
        {
            SetValuesOf_obj_sequenceNumber_sequenceIdentifier_CallCount++;
        }
    }
}