using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Tests.TestModel
{
    public class ImmutableObject
    {
        public string Prop1 { get; private set; }
        public string Prop2 { get; private set; }

        public ImmutableObject(string prop1, string prop2)
        {
            Prop1 = prop1;
            Prop2 = prop2;
        }
    }
}