namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class SimpleClass
    {
        public int PropA { get; set; }
        public int PropB { get; set; }
        public int PropC { get; set; }

        public string String1 { get; set; }
        public string String2 { get; set; }

        public SimpleClass()
        {
            
        }

        public SimpleClass(string string1)
        {
            this.String1 = string1;
        }
    }
}