using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyClassWithPrivateParameterizedConstructor
    {
        int a;
        private MyClassWithPrivateParameterizedConstructor(int a)
        {
            this.a = a;
        }

        public static MyClassWithPrivateParameterizedConstructor Instance()
        {
            return new MyClassWithPrivateParameterizedConstructor(100);
        }
    }
}
