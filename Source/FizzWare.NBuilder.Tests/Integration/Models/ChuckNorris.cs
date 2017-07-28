namespace FizzWare.NBuilder.Tests.Integration.Models
{
    //Only Chuck Norris can instantiate himself
    public class ChuckNorris
    {
        private int a;
        private ChuckNorris(int a)
        {
            this.a = a;
        }

        public static ChuckNorris Instance()
        {
            return new ChuckNorris(100);
        }
    }
}
