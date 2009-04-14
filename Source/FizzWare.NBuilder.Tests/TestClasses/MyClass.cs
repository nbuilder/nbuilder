using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Tests.TestClasses
{
    public class MyClass
    {
        private string _hasADefaultValue = "DefaultValue";
        public string HasADefaultValue
        {
            get { return _hasADefaultValue; }
            set { _hasADefaultValue = value; }
        }

        public IList<SimpleClass> SimpleClasses { get; set; }

        public MyClass()
        {
            SimpleClasses = new List<SimpleClass>();
        }

        public string StringOne { get; set; }
        public string StringTwo { get; set; }
        
        public byte Byte { get; set; }
        public short Short { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public decimal Decimal { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }

        public char Char { get; set; }

        public ushort Ushort { get; set; }
        public uint Uint { get; set; }
        public ulong Ulong { get; set; }

        public virtual void DoSomething()
        {
        }

        public void Add(SimpleClass simpleClass)
        {
            SimpleClasses.Add(simpleClass);
        }
    }
}