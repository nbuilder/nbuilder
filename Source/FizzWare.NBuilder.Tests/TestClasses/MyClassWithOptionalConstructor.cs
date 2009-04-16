namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyClassWithOptionalConstructor
    {
        public string Arg1 { get; set; }
        public int Arg2 { get; set; }

        public MyClassWithOptionalConstructor()
        {
            
        }

        public MyClassWithOptionalConstructor(string arg1, int arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }
}