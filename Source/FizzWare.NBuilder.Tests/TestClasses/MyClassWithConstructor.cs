namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyClassWithConstructor
    {
        public int Int { get; set; }
        public float Float { get; set; }
        public string String { get; set; }
        public decimal Decimal { get; set; }

        public MyClassWithConstructor(string aString, decimal aDecimal)
        {
            String = aString;
            Decimal = aDecimal;
        }

        public MyClassWithConstructor(int anInt, float aFloat)
        {
            Int = anInt;
            Float = aFloat;
        }
    }
}