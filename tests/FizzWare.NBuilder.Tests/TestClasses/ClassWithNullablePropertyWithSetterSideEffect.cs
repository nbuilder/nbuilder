namespace FizzWare.NBuilder.Tests.TestClasses
{
    /// <summary>
    /// Test class with a nullable property that has a setter with side effects.
    /// Used to verify that SetValue is actually called when setting nullable properties to null.
    /// </summary>
    public class ClassWithNullablePropertyWithSetterSideEffect
    {
        private int? _nullableInt;
        private System.Guid? _nullableGuid;

        /// <summary>
        /// Counts how many times the NullableInt setter was called
        /// </summary>
        public int NullableIntSetterCallCount { get; private set; }

        /// <summary>
        /// Counts how many times the NullableGuid setter was called
        /// </summary>
        public int NullableGuidSetterCallCount { get; private set; }

        public int? NullableInt
        {
            get => _nullableInt;
            set
            {
                NullableIntSetterCallCount++;
                _nullableInt = value;
            }
        }

        public System.Guid? NullableGuid
        {
            get => _nullableGuid;
            set
            {
                NullableGuidSetterCallCount++;
                _nullableGuid = value;
            }
        }

        /// <summary>
        /// Regular nullable property without side effects for comparison
        /// </summary>
        public decimal? NullableDecimal { get; set; }
    }
}
