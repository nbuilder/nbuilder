using System.Text.RegularExpressions;


namespace FizzWare.NBuilder.Generators
{
    public class StringGenerator : IGenerator<string>
    {
        public StringGenerator(int min, int max)
        {

        }

        public StringGenerator(string regex)
        {

        }

        public string Generate()
        {
            return "";
        }
    }
}