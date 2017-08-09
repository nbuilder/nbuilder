namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyClassWithPrivateParameterlessConstructor
    {
        //NOTE: For ORM and/or bulding objects with a given state for simplified unit testing
        private MyClassWithPrivateParameterlessConstructor() { }

        public MyClassWithPrivateParameterlessConstructor(int propA)
        {
            InvariantPropA = propA;
        }

        public int InvariantPropA { get; private set; }
    }
}
