using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Unit
{
    public interface IMyClassRepository
    {
        void Save(MyClass obj);
        void SaveAll(IList<MyClass> list);
    }

    public class PersisterRegistry
    {
        
    }
}