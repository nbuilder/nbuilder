using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.FunctionalTests.Model
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
