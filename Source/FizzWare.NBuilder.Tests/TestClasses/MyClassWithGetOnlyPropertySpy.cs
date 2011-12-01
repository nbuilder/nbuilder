using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyClassWithGetOnlyPropertySpy
    {
        public bool IsSet { get; private set; }
        public string PrivateSetProperty
        {
            get
            {
                IsSet = true;
                return IsSet.ToString();
            }
        }

    }
}
